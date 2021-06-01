using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CateringSystem.PayPal;

namespace CateringSystemTest
{
    [TestClass]
    public class PayPalClientTest
    {
        private const String _ClientId = "AW23DUoX2Yf-C4KXuJ_0wuwWAx3HQLhTyHbwAGtmexnrcT6qSsfPkLjs0SFHXQIYAldjRLNKRmzKCjhO";
        private const String _Secret = "EOzxJPOVv-mdfoJD4wGgoITfpei47V9etOfR5WX3gZa1oKnYa5Uv_w2JY_HDIGd04y-ISBL8O_aHmCcr";

        private static readonly PayPalOptions _SandboxOptions;

        static PayPalClientTest() => PayPalClientTest._SandboxOptions = new PayPalOptions(true, PayPalClientTest._ClientId, PayPalClientTest._Secret);

        [TestMethod]
        public void TestGetAccessToken()
        {
            PayPalClient client = new PayPalClient(PayPalClientTest._SandboxOptions);
            Task<String> task = Task.Run<String>(() => client.GetAccessTokenAsync());
            String accessToken = task.Result;
            Assert.IsNotNull(accessToken);
            Assert.IsFalse(String.IsNullOrWhiteSpace(accessToken));
            Console.WriteLine("WARNING: visual inspection required for access token " + accessToken);
        }
    }
}
