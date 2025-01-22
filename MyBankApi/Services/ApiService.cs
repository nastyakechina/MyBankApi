using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyBankApi.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5251/api";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetWalletBalanceAsync(Guid walletId)
        {
            var response = await _httpClient.GetFromJsonAsync<decimal>($"{BaseUrl}/CoinAmount/{walletId}/balance");
            return response;
        }

        public async Task DepositAsync(string currency, decimal amount)
        {
            // Фиксированный идентификатор кошелька
            var walletId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            var requestBody = new
            {
                Amount = amount,
                CoinName = currency,
                WalletId = walletId
            };

            var url = $"{BaseUrl}/CoinAmount/{walletId}/balance";

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            response.EnsureSuccessStatusCode();
        }


        public async Task ConvertCurrencyAsync(Guid walletId, string fromCurrency, string toCurrency, decimal amount)
        {
            var requestBody = new { FromCurrency = fromCurrency, ToCurrency = toCurrency, Amount = amount };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Transaction", requestBody);

            response.EnsureSuccessStatusCode();
        }

        public async Task AddCoinAsync(string currencyName, decimal amount)
        {
            var requestBody = new { Name = currencyName, Amount = amount };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Coin", requestBody);

            response.EnsureSuccessStatusCode();
        }

        public async Task<string[]> GetTransactionHistoryAsync()
        {
            return await _httpClient.GetFromJsonAsync<string[]>($"{BaseUrl}/Transaction");
        }
    }
}