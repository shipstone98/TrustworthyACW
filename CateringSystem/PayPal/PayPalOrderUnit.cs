using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CateringSystem.PayPal
{
    public class PayPalOrderUnit
    {
        [Key]
        public int ID { get; set; }

        public PayPalOrderIntent Intent { get; set; }
        public virtual ICollection<CartItem> Items { get; set; }

        public PayPalOrderUnit() { }

        internal PayPalOrderUnit(PayPalOrderIntent intent, IEnumerable<CartItem> items)
        {
            this.Intent = intent;
            this.Items = new List<CartItem>(items);
        }

        internal String Serialize() => this.Serialize(Encoding.Default);

        internal String Serialize(Encoding encoding)
        {
            using (Stream stream = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
                {
                    Decimal total = 0;

                    writer.WriteStartObject();
                        writer.WriteString("intent", this.Intent.ToString().ToUpper());
                        
                        writer.WriteStartArray("purchase_units");
                            writer.WriteStartObject();
                                writer.WriteStartArray("items");

                                foreach (CartItem item in this.Items)
                                {
                                    total += item.Quantity * item.Product.Price;
                                    writer.WriteStartObject();
                                        writer.WriteString("name", item.Product.Name);
                                        writer.WriteStartObject("unit_amount");
                                            writer.WriteString("currency_code", "GBP");
                                            writer.WriteString("value", item.Product.Price.ToString());
                                        writer.WriteEndObject();
                                        writer.WriteString("quantity", item.Quantity.ToString());
                                    writer.WriteEndObject();
                                }

                                writer.WriteEndArray();
                                writer.WriteStartObject("amount");
                                    writer.WriteString("currency_code", "GBP");
                                    writer.WriteString("value", total.ToString());
                                    writer.WriteStartObject("breakdown");
                                        writer.WriteStartObject("item_total");
                                            writer.WriteString("currency_code", "GBP");
                                            writer.WriteString("value", total.ToString());
                                        writer.WriteEndObject();
                                    writer.WriteEndObject();
                                writer.WriteEndObject();
                            writer.WriteEndObject();
                        writer.WriteEndArray();
                        writer.WriteStartObject("application_context");
                            writer.WriteString("return_url", "https://cateringsystem.azurewebsites.net/DisplayOrder");
                            writer.WriteString("cancel_url", "https://cateringsystem.azurewebsites.net/Index");
                        writer.WriteEndObject();
                    writer.WriteEndObject();
                    writer.Flush();

                    long position = stream.Position; 
                    stream.Seek(0, SeekOrigin.Begin);
                    int bufsiz = (int) Math.Min(position, Int32.MaxValue);
                    byte[] buffer = new byte[bufsiz];
                    StringBuilder sb = new StringBuilder();
                    
                    while (position > 0)
                    {
                        int n = stream.Read(buffer, 0, bufsiz);
                        position -= n;
                        sb.Append(encoding.GetString(buffer, 0, n));
                    }

                    return sb.ToString();
                }
            }
        }
    }
}