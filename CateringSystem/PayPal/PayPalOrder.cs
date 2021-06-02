using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using CateringSystem.PayPal.Internals;

namespace CateringSystem.PayPal
{
    public class PayPalOrder
    {
        internal readonly ICollection<InternalOrderLink> _Links;

        public String ID { get; set; }

        private PayPalOrder(String id)
        {
            this._Links = new List<InternalOrderLink>();
            this.ID = id;
        }

        internal static PayPalOrder Deserialize(String jsonString) => PayPalOrder.Deserialize(jsonString, Encoding.Default);

        internal static PayPalOrder Deserialize(String jsonString, Encoding encoding)
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
                        }

                        else if (property.NameEquals("status"))
                        {
                            if (!property.Value.GetString().Equals("CREATED"))
                            {
                                throw new FormatException();
                            }
                        }

                        else if (property.NameEquals("links"))
                        {
                            foreach (JsonElement linkElement in property.Value.EnumerateArray())
                            {
                                String href = null, rel = null;
                                HttpMethod method = null;

                                foreach (JsonProperty linkProperty in linkElement.EnumerateObject())
                                {
                                    if (linkProperty.NameEquals("href"))
                                    {
                                        if (!(href is null))
                                        {
                                            throw new FormatException();
                                        }

                                        href = linkProperty.Value.GetString();
                                    }

                                    else if (linkProperty.NameEquals("rel"))
                                    {
                                        if (!(rel is null))
                                        {
                                            throw new FormatException();
                                        }

                                        rel = linkProperty.Value.GetString();
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
                                            method = HttpMethod.Delete;
                                            break;
                                        case "GET":
                                            method = HttpMethod.Get;
                                            break;
                                        case "HEAD":
                                            method = HttpMethod.Head;
                                            break;
                                        case "OPTIONS":
                                            method = HttpMethod.Options;
                                            break;
                                        case "PATCH":
                                            method = HttpMethod.Patch;
                                            break;
                                        case "POST":
                                            method = HttpMethod.Post;
                                            break;
                                        case "PUT":
                                            method = HttpMethod.Put;
                                            break;
                                        case "TRACE":
                                            method = HttpMethod.Trace;
                                            break;
                                        default:
                                            throw new FormatException();
                                        }
                                    }
                                }

                                if (href is null || rel is null || method is null)
                                {
                                    throw new FormatException();
                                }

                                order._Links.Add(new InternalOrderLink(href, rel, method));
                            }
                        }
                    }
                }
            }

            catch (JsonException)
            {
                throw new FormatException();
            }

            return order;
        }
    }
}