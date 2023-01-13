using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using MagicVilla_Web.Utility;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }

        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.ResponseModel = new();
            this.HttpClient = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MagicAPI");

                HttpRequestMessage message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data!= null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(
                            apiRequest.Data,
                            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), // Ignoring nulls to prevent parsing issues when serializing null numbers.
                        Encoding.UTF8,
                        "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;

                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        throw new Exception("HTTP method not supported.");
                }

                HttpResponseMessage httpResponse = await client.SendAsync(message);

                var apiContent = await httpResponse.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<T>(apiContent); // Serializing and Deserializing the API Response is required so the response is ultimately sent as type T.

                return apiResponse;
            }
            catch(Exception ex)
            {
                var apiResponse = new APIResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };

                var apiResponseSerialized = JsonConvert.SerializeObject(apiResponse);

                var apiResponseDeserialized = JsonConvert.DeserializeObject<T>(apiResponseSerialized); // Serializing and Deserializing the API Response is required so the response is ultimately sent as type T.

                return apiResponseDeserialized;
            }
        }
    }
}