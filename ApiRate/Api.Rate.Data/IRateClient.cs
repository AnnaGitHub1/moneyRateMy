using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Rate.Data
{
    public interface IRateClient
    {
        Task<decimal> GetRate(string currencySrc, string currencyDest);

        Task<List<CurrencyRate>> GetAllRates(string currency);
    }
}
