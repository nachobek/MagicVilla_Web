using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;

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

        public Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MagicAPI");

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

            }
        }
    }
}