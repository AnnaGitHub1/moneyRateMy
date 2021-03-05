using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Rate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Rate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> _logger;

        private readonly IRateService _rateService;

        public RateController(ILogger<RateController> logger, 
            IRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [HttpGet, Route("{currencySrc}")]
        public async Task<IActionResult> Get(string currencySrc)
        {
            var rates = await _rateService.GetAllRates(currencySrc);

            if (!rates.Any())
            {
                return BadRequest();
            }
            return Ok(rates);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string currencySrc, string currencyDest)
        {
            var rate = await _rateService.GetRate(currencySrc, currencyDest);

            if(rate == null)
            {
                return BadRequest();
            }
            return Ok(rate);
        }
    }
}
