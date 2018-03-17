using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PJE.WatchOrders.Models;

namespace PJE.WatchOrders.Data
{
    public class MemoryRepository : IRepository
    {
        private readonly IList<Order> _orders = new List<Order>();

        private readonly IList<Item> _items = new List<Item>();

        private readonly IList<Shipping> _shipping = new List<Shipping>();

        public Task AddOrderAsync(Order order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public async Task AddOrdersAsync(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                await AddOrderAsync(order);
            }
        }

        public Task AddItemAsync(Item item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public async Task AddItemsAsync(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                await AddItemAsync(item);
            }
        }

        public Task AddShippingAsync(Shipping shipping)
        {
            _shipping.Add(shipping);
            return Task.CompletedTask;
        }

        public async Task AddShippingsAsync(IEnumerable<Shipping> shippings)
        {
            foreach (var shipping in shippings)
            {
                await AddShippingAsync(shipping);
            }
        }

        public async Task<IEnumerable<OrderLine>> GetCompletedLinesAsync()
            => GetCompletedLines();

        private IEnumerable<OrderLine> GetCompletedLines()
        {
            var xs = _orders
                .Join(
                    _shipping,
                    o => new {o.Marketplace, o.OrderReference},
                    s => new {s.Marketplace, s.OrderReference},
                    (o, s) => new {Order = o, Shipping = s})
                .Join(
                    _items,
                    o => new {o.Order.Marketplace, o.Order.OrderReference},
                    i => new {i.Marketplace, i.OrderReference},
                    (o, i) => new {o.Order, o.Shipping, Item = i});

            foreach (var x in xs)
            {
                // TODO: check that this doesn't cause problems
                // (editing while looping)
                _items.Remove(x.Item);
                yield return new OrderLine(x.Order, x.Shipping, x.Item);
            }
        }
    }
}
