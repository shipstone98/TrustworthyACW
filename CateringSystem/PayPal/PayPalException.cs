using System;
using System.Collections.Generic;

namespace CateringSystem.PayPal
{
    public class PayPalException : Exception
    {
        internal const int _GetTokenError = 1000;
        internal const int _OrderCreationError = 1001;

        private static readonly IReadOnlyDictionary<int, String> _ErrorMessages;

        public bool IsSandbox { get; }

        static PayPalException() => PayPalException._ErrorMessages = new Dictionary<int, String>
        {
            { PayPalException._GetTokenError, "Error occurred when retrieving an access token" },
            { PayPalException._OrderCreationError, "Error occurred when creating an order request" }
        };

        internal PayPalException(int id, bool isSandbox) : base(PayPalException._ErrorMessages[id]) => this.IsSandbox = isSandbox;
    }
}