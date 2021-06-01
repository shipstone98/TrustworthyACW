using System;

namespace CateringSystem.PayPal
{
    public class PayPalOptions
    {
        public String ClientId { get; }
        public bool IsSandbox { get; }
        public String Secret { get; }

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