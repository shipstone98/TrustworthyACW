using System;
using System.Net.Http;

namespace CateringSystem.PayPal.Internals
{
    internal class InternalOrderLink
    {
        internal readonly String _HyperTextReference;
        internal readonly HttpMethod _Method;
        internal readonly String _Relationship;

        internal InternalOrderLink(String href, String rel, HttpMethod method)
        {
            this._HyperTextReference = href;
            this._Method = method;
            this._Relationship = rel;
        }
    }
}