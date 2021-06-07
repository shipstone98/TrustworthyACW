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
            "AUU72lCO6G3RnKH-IB7-K2vWjhwyYVEmYSEJYXw1q0hWqLvmlRYxUQbISNaM_yusaJjc5KsmJTpV9FC3",
            "EKd_kvFhPjMILd3zMMNUMvrzlvJFKwKDfc0h7BskQFytYMsBb3S-pDMkKUuPRhqQ3FJG0OqZ86TAj82K"
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