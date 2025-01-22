using MyBankApi.Services;
using System.Net.Http;

namespace MyBankApi
{
    public static class ServiceLocator
    {
        private static readonly HttpClient _httpClient = new();
        public static ApiService ApiService { get; } = new ApiService(_httpClient);
    }
}