using Newtonsoft.Json;

namespace PJE.WatchOrders.Models
{
    public class Item
    {
        [JsonProperty("order reference")]
        public string OrderReference { get; set; }

        public string Marketplace { get; set; }

        [JsonProperty("order item number")]
        public string OrderItemNumber { get; set; }

        public string Sku { get; set; }

        [JsonProperty("price per unit")]
        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }
    }
}
