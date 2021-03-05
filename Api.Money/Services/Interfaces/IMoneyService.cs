using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMoneyService
    {
        /// <summary>
        /// Получить все кошельки пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<MoneyDto>> UserWallets(string userId);

        /// <summary>
        /// Положить деньги
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="sum"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<decimal> PutMoney(long walletId, decimal sum, string currency);

        /// <summary>
        /// Снять деньги
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="sum"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<decimal> WithdrawMoney(long walletId, decimal sum, string currency);

        /// <summary>
        /// Поменять валютность кошелька
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="currencyDest"></param>
        /// <returns></returns>
        Task<decimal> TransferToAnotherCurrency(long walletId, string currencyDest);
    }
}
