using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Api.Rate.Data
{
    public class EuropaClient : IRateClient
    {
        private readonly string bankUri;
        private readonly HttpClient httpClient = new HttpClient();

        public EuropaClient(IOptions<BankSettings> options)
        {
            bankUri = options.Value.BankUri;
        }

        public async Task<decimal> GetRate(string currencySrc, string currencyDest)
        {
            var cubes = await GetCubes();

            var srcRate = cubes.FirstOrDefault(source => source.Currency == currencySrc)?.Rate ?? 1;

            var destRate = cubes.FirstOrDefault(dest => dest.Currency == currencyDest)?.Rate ?? 1;

            return srcRate / destRate;

        }

        public async Task<List<CurrencyRate>> GetAllRates(string currency)
        {
            var cubes = await GetCubes();

            var srcRate = cubes.FirstOrDefault(source => source.Currency == currency)?.Rate ?? 1;

            var currencyList = cubes.Select(s => new CurrencyRate
            {
                Currency = s.Currency,
                Rate = (decimal)srcRate / s.Rate
            })
            .ToList();

            //евро
            currencyList.Add(new CurrencyRate { Currency = "EUR", Rate = srcRate });

            return currencyList;
        }

        private async Task<Cube[]> GetCubes()
        {
            using var request = await httpClient.GetAsync(bankUri);

            var response = await request.Content.ReadAsStreamAsync();

            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));

            var envelope = (Envelope)serializer.Deserialize(response);

            return envelope.Cube.Cubes;
        }
    }
}
