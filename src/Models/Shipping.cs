using Newtonsoft.Json;

namespace PJE.WatchOrders.Models
{
    public class Shipping
    {
        [JsonProperty("order reference")]
        public string OrderReference { get; set; }

        public string Marketplace { get; set; }

        [JsonProperty("postal service")]
        public string PostalService { get; set; }

        public string Postcode { get; set; }
    }
}
