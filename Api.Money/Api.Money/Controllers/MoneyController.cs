using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Money.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MoneyController : Controller
    {
        private readonly IMoneyService _moneyService;

        public MoneyController(IMoneyService moneyService)
        {
            _moneyService = moneyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            var wallets = await _moneyService.UserWallets(GetUserId());

            return Ok(wallets);
        }

        [HttpPost]
        public async Task<IActionResult> PutMoney(long walletId, decimal sum, string currency)
        {
            var wallets = await _moneyService.PutMoney(walletId, sum, currency);

            return Ok(wallets);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawMoney(long walletId, decimal sum, string currency)
        {
            var wallets = await _moneyService.WithdrawMoney(walletId, sum, currency);

            return Ok(wallets);
        }

        [HttpPost("changeCurrency")]
        public async Task<IActionResult> ChangeCurrency(long walletId, string currency)
        {
            var wallets = await _moneyService.TransferToAnotherCurrency(walletId, currency);

            return Ok(wallets);
        }


        private string GetUserId()
        {
            var us = (ClaimsIdentity)User.Identity;

            return us.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;

        }
    }
}