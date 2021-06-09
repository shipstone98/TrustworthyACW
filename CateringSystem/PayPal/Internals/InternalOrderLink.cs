using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem.PayPal.Internals
{
    public class InternalOrderLink
    {
        public String HyperTextReference { get; set; }

        [Key]
        public int ID { get; set; }
        
        public HttpMethodType Method { get; set; }
        public String Relationship { get; set; }

        public InternalOrderLink() { }

        internal InternalOrderLink(String href, String rel, HttpMethodType method)
        {
            this.HyperTextReference = href;
            this.Method = method;
            this.Relationship = rel;
        }
    }
}