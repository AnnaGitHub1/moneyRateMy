using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Api.Rate.Data.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var europeClient = new EuropaClient(null);

            var aa = await europeClient.GetAllRates("RUB");

            var bb = await europeClient.GetRate("RUB", "EUR");

            Assert.Pass();
        }
    }
}