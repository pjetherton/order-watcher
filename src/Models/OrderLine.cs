using CsvHelper.Configuration.Attributes;

namespace PJE.WatchOrders.Models
{
    public class OrderLine
    {
        public OrderLine(Order o, Shipping s, Item i)
        {
            OrderReference = o.OrderReference;
            Marketplace = o.Marketplace;
            Name = o.Name;
            Surname = o.Surname;
            OrderItemNumber = i.OrderItemNumber;
            Sku = i.Sku;
            PricePerUnit = i.PricePerUnit;
            Quantity = i.Quantity;
            PostalService = s.PostalService;
            Postcode = s.Postcode;
        }

        [Name("order reference")]
        [Index(0)]
        public string OrderReference { get; }

        [Name("marketplace")]
        [Index(1)]
        public string Marketplace { get; }

        [Name("name")]
        [Index(2)]
        public string Name { get; }

        [Name("surname")]
        [Index(3)]
        public string Surname { get; }

        [Name("order item number")]
        [Index(4)]
        public string OrderItemNumber { get; }

        [Name("sku")]
        [Index(5)]
        public string Sku { get; }

        [Name("price per unit")]
        [Index(6)]
        public decimal PricePerUnit { get; }

        [Name("quantity")]
        [Index(7)]
        public int Quantity { get; }

        [Name("postal service")]
        [Index(8)]
        public string PostalService { get; }

        [Name("postcode")]
        [Index(9)]
        public string Postcode { get; }
    }
}
