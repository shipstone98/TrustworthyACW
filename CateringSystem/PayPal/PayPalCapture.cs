using System;
using System.Text;
using System.Text.Json;

namespace CateringSystem.PayPal
{
    public class PayPalCapture : PayPalOrder
    {
        public bool IsCompleted { get; private set; }

        internal PayPalCapture(PayPalOrder order) : base(order) { }

        internal new static PayPalCapture Deserialize(String jsonString) => PayPalCapture.Deserialize(jsonString, Encoding.Default);

        internal new static PayPalCapture Deserialize(String jsonString, Encoding encoding)
        {
            PayPalCapture capture = PayPalOrder.Deserialize(jsonString, encoding, true) as PayPalCapture;

            try
            {
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    foreach (JsonProperty rootProperty in document.RootElement.EnumerateObject())
                    {
                        if (rootProperty.NameEquals("status"))
                        {
                            capture.IsCompleted = rootProperty.Value.GetString().Equals("COMPLETED");
                        }
                    }
                }
            }

            catch
            {
                throw new FormatException();
            }

            return capture;
        }
    }
}