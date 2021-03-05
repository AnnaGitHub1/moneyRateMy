using Api.Rate.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Rate
{
    public interface IRateService
    {
        Task<decimal?> GetRate(string currencySrc, string currencyDest);

        Task<List<CurrencyRate>> GetAllRates(string currency);
    }
}
