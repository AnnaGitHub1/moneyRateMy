using Newtonsoft.Json;

namespace Models
{
    public class CurrencyRate
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
