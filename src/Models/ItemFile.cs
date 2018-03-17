using System.Collections.Generic;

using Newtonsoft.Json;

namespace PJE.WatchOrders.Models
{
    public class ItemFile
    {
        [JsonProperty("order items")]
        public IList<Item> OrderItems { get; set; }
    }
}
