using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

using CateringSystem.PayPal.Internals;

namespace CateringSystem.PayPal
{
    public class PayPalOrder
    {
        public String ApproveLink { get; set; }

        [Key]
        public String ID { get; set; }
        
        public virtual ICollection<InternalOrderLink> Links { get; set; }

        public PayPalOrder() { }

        private PayPalOrder(String id)
        {
            this.Links = new List<InternalOrderLink>();
            this.ID = id;
        }

        private protected PayPalOrder(PayPalOrder order)
        {
            this.Links = new List<InternalOrderLink>(order.Links);
            this.ID = order.ID;
        }

        internal static PayPalOrder Deserialize(String jsonString) => PayPalOrder.Deserialize(jsonString, Encoding.Default);
        internal static PayPalOrder Deserialize(String jsonString, Encoding encoding) => PayPalOrder.Deserialize(jsonString, encoding, false);

        private protected static PayPalOrder Deserialize(String jsonString, Encoding encoding, bool isCapture)
        {
            PayPalOrder order = null;

            try
            {
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    foreach (JsonProperty property in document.RootElement.EnumerateObject())
                    {
                        if (property.NameEquals("id"))
                        {
                            if (!(order is null))
                            {
                                throw new FormatException();
                            }

                            order = new PayPalOrder(property.Value.GetString());

                            if (isCapture)
                            {
                                order = new PayPalCapture(order);
                            }
                        }

                        else if (property.NameEquals("status"))
                        {
                            if (isCapture)
                            {
                                if (!property.Value.GetString().Equals("COMPLETED"))
                                {
                                    throw new FormatException();
                                }
                            }

                            else if (!property.Value.GetString().Equals("CREATED"))
                            {
                                throw new FormatException();
                            }
                        }

                        else if (property.NameEquals("links"))
                        {
                            foreach (JsonElement linkElement in property.Value.EnumerateArray())
                            {
                                String href = null, rel = null;
                                Nullable<HttpMethodType> method = null;

                                foreach (JsonProperty linkProperty in linkElement.EnumerateObject())
                                {
                                    if (linkProperty.NameEquals("href"))
                                    {
                                        if (!(href is null))
                                        {
                                            throw new FormatException();
                                        }

                                        href = linkProperty.Value.GetString();

                                        if (!(rel is null) && rel.Equals("approve"))
                                        {
                                            order.ApproveLink = href;
                                        }
                                    }

                                    else if (linkProperty.NameEquals("rel"))
                                    {
                                        if (!(rel is null))
                                        {
                                            throw new FormatException();
                                        }

                                        if (!(href is null) && (rel = linkProperty.Value.GetString()).Equals("approve"))
                                        {
                                            order.ApproveLink = href;
                                        }
                                    }

                                    else if (linkProperty.NameEquals("method"))
                                    {
                                        if (!(method is null))
                                        {
                                            throw new FormatException();
                                        }

                                        String str = linkProperty.Value.GetString();

                                        switch (str)
                                        {
                                        case "DELETE":
                                            method = HttpMethodType.Delete;
                                            break;
                                        case "GET":
                                            method = HttpMethodType.Get;
                                            break;
                                        case "HEAD":
                                            method = HttpMethodType.Head;
                                            break;
                                        case "OPTIONS":
                                            method = HttpMethodType.Options;
                                            break;
                                        case "PATCH":
                                            method = HttpMethodType.Patch;
                                            break;
                                        case "POST":
                                            method = HttpMethodType.Post;
                                            break;
                                        case "PUT":
                                            method = HttpMethodType.Put;
                                            break;
                                        case "TRACE":
                                            method = HttpMethodType.Trace;
                                            break;
                                        default:
                                            throw new FormatException();
                                        }
                                    }
                                }

                                if (href is null || rel is null || method is null || !method.HasValue)
                                {
                                    throw new FormatException();
                                }

                                order.Links.Add(new InternalOrderLink(href, rel, method.Value));
                            }
                        }
                    }
                }
            }

            catch (JsonException)
            {
                throw new FormatException();
            }

            if (!isCapture && order.ApproveLink is null)
            {
                throw new FormatException();
            }

            return order;
        }
    }
}