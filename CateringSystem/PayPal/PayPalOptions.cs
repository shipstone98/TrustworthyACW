using System;

namespace CateringSystem.PayPal
{
    public class PayPalOptions
    {
        public static readonly PayPalOptions Default;

        public String ClientId { get; }
        public bool IsSandbox { get; }
        public String Secret { get; }

        static PayPalOptions() => PayPalOptions.Default = new PayPalOptions(
            true,
            "AW23DUoX2Yf-C4KXuJ_0wuwWAx3HQLhTyHbwAGtmexnrcT6qSsfPkLjs0SFHXQIYAldjRLNKRmzKCjhO",
            "EOzxJPOVv-mdfoJD4wGgoITfpei47V9etOfR5WX3gZa1oKnYa5Uv_w2JY_HDIGd04y-ISBL8O_aHmCcr"
        );

        public PayPalOptions(bool isSandbox, String clientId, String secret)
        {
            if (clientId is null)
            {
                throw new ArgumentNullException(nameof (clientId));
            }

            if (secret is null)
            {
                throw new ArgumentNullException(nameof (secret));
            }

            this.ClientId = clientId;
            this.IsSandbox = isSandbox;
            this.Secret = secret;
        }
    }
}