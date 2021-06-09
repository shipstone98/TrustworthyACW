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
            "Abc-ZQGi2_BdV4BIukZCwmda4zzT57-54OAsA_0hEZeB9O1S2nI5-nBG8yxEgEEJeawapP6H4sBWAuxX",
            "ENT0PXEHZ6aliUtRpDzY-FzfgwJ8SEEA8ji367zMYjvLy8An1XH4QllGBXDEItGXMUqWJSGVQWxSCOMr"
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