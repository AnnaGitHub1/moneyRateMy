using Microsoft.Extensions.Options;
using Models;
using Models.Settings;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class ApiRateClient : IRateService
    {
        private readonly HttpClient httpClient = new HttpClient();

        private readonly string url;

        public ApiRateClient(IOptions<ApiRate> options)
        {
            url = options.Value.Url;
        }

        public async Task<List<CurrencyRate>> GetAllRates(string currency)
        {
            using var request = await httpClient.GetAsync($"{url}/{currency}");

            var response = await request.Content.ReadAsStringAsync();

            var currencyRateList = JsonConvert.DeserializeObject<List<CurrencyRate>>(response);

            return currencyRateList;
        }

        public async Task<decimal> GetRate(string currencySrc, string currencyDest)
        {
            using var request = await httpClient.GetAsync($"{url}/?currencySrc={currencySrc}&currencyDest={currencyDest}");

            var response = await request.Content.ReadAsStringAsync();

            var rate = JsonConvert.DeserializeObject<decimal>(response);

            return rate;
        }
    }
}
