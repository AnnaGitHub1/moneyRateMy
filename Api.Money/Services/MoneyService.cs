using Data;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using System.Collections.Concurrent;

namespace Services
{
    public class MoneyService : IMoneyService
    {
        private readonly WalletContext _walletContext;
        private readonly IRateService _rateService;

        public MoneyService(
            WalletContext walletContext,
            IRateService rateService)
        {
            _walletContext = walletContext;
            _rateService = rateService;
        }
        
        public async Task<List<MoneyDto>> UserWallets(string userId)
        {
            var userIdLong = Convert.ToInt64(userId);

            var wallets =  await _walletContext.Money
                .Include(i => i.Status)
                .Include(i => i.Currency)
                .Where(w => w.UserId == userIdLong)
                .ToListAsync();

            if(wallets == null)
            {
                return null;
            }

            var tasks = new List<Task>();
            var currencyRate = new ConcurrentBag<(string, List<CurrencyRate>)>();

            var walletGroup = wallets.GroupBy(g => new { g.Currency.CurrencyName }).Select(s => s.Key);

            foreach (var wallet in walletGroup)
            {
                var rates = _rateService.GetAllRates(wallet.CurrencyName).ContinueWith(c =>

                currencyRate.Add((wallet.CurrencyName, c.Result))

                );

                tasks.Add(rates);
            }

            await Task.WhenAll(tasks);

            var moneyDtoList = wallets.Select(s => new MoneyDto
            {
                Id = s.Id,
                MoneyStatus = s.Status.StatusName,
                UserId = s.UserId,
                Balances = currencyRate.FirstOrDefault(w => w.Item1 == s.Currency.CurrencyName).Item2
                .Select(
                    rate => new BalanceDto
                    {
                        Currency = rate.Currency,
                        Sum = Math.Round(s.Sum / rate.Rate, 2)
                    })
                .ToList()
                
            })
            .ToList();
                       
            return moneyDtoList;
        }

         public async Task<decimal> PutMoney(long walletId, decimal sum, string currency)
        {
            var wallet = await _walletContext.Money
                .Include(i => i.Currency)
                .Include(i => i.Status)
                .SingleOrDefaultAsync(s => s.Id == walletId);

            if(!IsValidPutMoneyWallet(wallet))
            {
                throw new NotActiveWalletException();
            }

            //существование валюты
            if (!IsCurrencyExist(currency))
            {
                throw new ExistCurrencyException();
            }

            sum = await ConvertSumToCurrency(sum, wallet.Currency.CurrencyName, currency);

            wallet.Sum += sum;

            await _walletContext.SaveChangesAsync();

            return wallet.Sum;
        }

        private bool IsValidPutMoneyWallet(Money wallet)
        {
            return wallet.Status.Id == (long)Models.Status.Active;           
        }

        public async Task<decimal> WithdrawMoney(long walletId, decimal sum, string currency)
        {
            var wallet = await _walletContext.Money
                .Include(i => i.Currency)
                .Include(i => i.Status)
                .SingleOrDefaultAsync(s => s.Id == walletId);

            if (!IsValidPutMoneyWallet(wallet))
            {
                throw new NotActiveWalletException();
            }            

            //существование валюты
            if(IsCurrencyExist(currency))
            {
                throw new ExistCurrencyException();
            }

            sum = await ConvertSumToCurrency(sum, wallet.Currency.CurrencyName, currency);

            if (!IsValidWithdrawMoney(wallet, sum))
            {
                throw new InsufficientFundsException();
            }

            wallet.Sum -= sum;

            await _walletContext.SaveChangesAsync();

            return wallet.Sum;
        }

        private bool IsValidWithdrawMoney(Money wallet, decimal sum)
        {
            return wallet.Sum >= Math.Round(sum, 2);
        }

        private bool IsCurrencyExist(string currencyDest)
        {
            return _walletContext.Currencies.Any(f => currencyDest!= null && f.CurrencyName.ToLower() == currencyDest.ToLower());
        }

        public async Task<decimal> TransferToAnotherCurrency(long walletId, string currencyDest)
        {
            var wallet = await _walletContext.Money
               .Include(i => i.Currency)
               .Include(i => i.Status)
               .SingleOrDefaultAsync(s => s.Id == walletId);

            if (!IsCurrencyExist(currencyDest))
            {
                throw new ExistCurrencyException();
            }

            var rate = await _rateService.GetRate(wallet.Currency.CurrencyName, currencyDest);
            
            //поменять сумму кошелька
            wallet.Sum = Math.Round(wallet.Sum / rate, 2);

            //поменять валютность кошелька
            wallet.CurrencyId = _walletContext.Currencies.FirstOrDefault(f => f.CurrencyName == currencyDest).Id;

            await _walletContext.SaveChangesAsync();

            return wallet.Sum;
        }

        private async Task<decimal> ConvertSumToCurrency(decimal sum, string currencyStr, string currencyDest)
        {
            if (currencyStr != currencyDest)
            {
                var rate = await _rateService.GetRate(currencyStr, currencyDest);

                sum *= rate;
            }

            return Math.Round(sum, 2);
        }
    }
}
