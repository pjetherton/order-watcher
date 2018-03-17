using Newtonsoft.Json;

namespace PJE.WatchOrders.Models
{
    public class Order
    {
        [JsonProperty("order reference")]
        public string OrderReference { get; set; }
        
        public string Marketplace { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
    }
}
