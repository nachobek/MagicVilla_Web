using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using MagicVilla_Web.Utility;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _villaUrl;

        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = _villaUrl + "/api/VillaNumberAPI/VillaNumbers"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = _villaUrl + $"/api/VillaNumberAPI/VillaNumbers/{id}"
            });
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO villaCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = _villaUrl + "/api/VillaNumberAPI/VillaNumbers",
                Data = villaCreateDTO
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = _villaUrl + $"/api/VillaNumberAPI/VillaNumbers/{villaUpdateDTO.VillaNo}",
                Data = villaUpdateDTO
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = _villaUrl + $"/api/VillaNumberAPI/VillaNumbers/{id}"
            });
        }
    }
}
