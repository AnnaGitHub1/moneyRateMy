using Api.Rate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Rate
{
    public class RateService : IRateService
    {
        private readonly IRateClient _rateClient;

        public RateService(IRateClient rateClient)
        {
            _rateClient = rateClient;
        }

        public async Task<decimal?> GetRate(string currencySrc, string currencyDest)
        {
            var rate = await _rateClient.GetRate(currencySrc, currencyDest);
            return rate;//Math.Round(rate, 4);
        }

        public async Task<List<CurrencyRate>> GetAllRates(string currency)
        {
            var rates = await _rateClient.GetAllRates(currency);

            foreach(var rate in rates)
            {
                rate.Rate = rate.Rate;//Math.Round(rate.Rate, 4);
            }

            return rates;
        }
    }
}
