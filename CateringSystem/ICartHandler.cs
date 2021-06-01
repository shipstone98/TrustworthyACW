using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CateringSystem
{
    public interface ICartHandler
    {
        String ID { get; }
        IEnumerable<CartItem> Items { get; }

        Task AddCartItemAsync(CartItem cartItem);
        Product GetProduct(int id);
        Task SaveChangesAsync();
    }
}