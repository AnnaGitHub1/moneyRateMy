using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRateService
    {
        /// <summary>
        /// Возвращает все имеющиеся курсы по данной валюте
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<List<CurrencyRate>> GetAllRates(string currency);

        /// <summary>
        /// Возвращает курс текущих валют
        /// </summary>
        /// <param name="currencySrc"></param>
        /// <param name="currencyDest"></param>
        /// <returns></returns>
        Task<decimal> GetRate(string currencySrc, string currencyDest);
    }
}
