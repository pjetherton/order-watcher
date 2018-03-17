using System.Collections.Generic;
using System.Threading.Tasks;
using PJE.WatchOrders.Models;

namespace PJE.WatchOrders.Data
{
    public interface IRepository
    {
        Task AddOrderAsync(Order order);

        Task AddOrdersAsync(IEnumerable<Order> orders);

        Task AddItemAsync(Item item);

        Task AddItemsAsync(IEnumerable<Item> items);

        Task AddShippingAsync(Shipping shipping);

        Task AddShippingsAsync(IEnumerable<Shipping> shippings);

        Task<IEnumerable<OrderLine>> GetCompletedLinesAsync();
    }
}
