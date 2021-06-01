using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CateringSystem;
using CateringSystem.PayPal;

namespace CateringSystemTest
{
    [TestClass]
    public class PayPalClientTest
    {
        private const String _ClientId = "AW23DUoX2Yf-C4KXuJ_0wuwWAx3HQLhTyHbwAGtmexnrcT6qSsfPkLjs0SFHXQIYAldjRLNKRmzKCjhO";
        private const String _Secret = "EOzxJPOVv-mdfoJD4wGgoITfpei47V9etOfR5WX3gZa1oKnYa5Uv_w2JY_HDIGd04y-ISBL8O_aHmCcr";

        private static readonly PayPalOptions _SandboxOptions;

        private PayPalClient _Client;

        static PayPalClientTest() => PayPalClientTest._SandboxOptions = new PayPalOptions(true, PayPalClientTest._ClientId, PayPalClientTest._Secret);

        [TestInitialize]
        public void Initialize() => this._Client = new PayPalClient(PayPalClientTest._SandboxOptions);

        [TestMethod]
        public void TestGetAccessToken()
        {
            Task<String> task = Task.Run<String>(() => this._Client.GetAccessTokenAsync());
            task.Wait();
            String accessToken = task.Result;
            Assert.IsNotNull(accessToken);
            Assert.IsFalse(String.IsNullOrWhiteSpace(accessToken));
            Console.WriteLine("WARNING: visual inspection required for access token " + accessToken);
        }

        [TestMethod]
        public void TestOrder()
        {
            Task tokenTask = Task.Run<String>(() => this._Client.GetAccessTokenAsync());
            ICollection<CartItem> cartItems = new List<CartItem>();
            Random random = new Random();
            const int MAX_QUANTITY = 5;

            foreach (Product product in MockContext._Products)
            {
                int quantity = random.Next(1, MAX_QUANTITY);

                CartItem cartItem = new CartItem
                {
                    Product = product,
                    ProductId = product.ID,
                    Quantity = quantity
                };

                cartItems.Add(cartItem);
            }

            tokenTask.Wait();
            Task task = Task.Run(() => this._Client.OrderAsync(cartItems));
            task.Wait();
        }
    }
}
