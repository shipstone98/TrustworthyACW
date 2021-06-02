using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CateringSystem.Logging
{
    public struct Log
    {
        private const String _DefaultDateFormat = "dd/MMM/yyyy HH:mm:ss zzz";

        private readonly String _RequestNoWhiteSpace;

        public String AuthorizedUser { get; }
        public int Bytes { get; }
        public DateTime Date => this.UtcDate.ToLocalTime();
        public String Host { get; }
        public String Identifier { get; }
        public String Request { get; }
        public HttpStatusCode Status { get; }
        public DateTime UtcDate { get; }

        public Log(String host, String ident, String authUser, String request, HttpStatusCode status, int bytes)
        {
            if (bytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof (bytes));
            }

            bool isRequestMissing = String.IsNullOrWhiteSpace(request);
            
            if (isRequestMissing)
            {
                this._RequestNoWhiteSpace = null;
            }

            else
            {
                this._RequestNoWhiteSpace = Regex.Replace(request, @"[ ]+", "\0");
                this._RequestNoWhiteSpace = Regex.Replace(this._RequestNoWhiteSpace, @"[\s]+", String.Empty);
            }

            this.AuthorizedUser = String.IsNullOrWhiteSpace(authUser) ? null : authUser;
            this.Bytes = bytes;
            this.Host = String.IsNullOrWhiteSpace(host) ? null : host;
            this.Identifier = String.IsNullOrWhiteSpace(ident) ? null : ident;
            this.Request = isRequestMissing ? null : request;
            this.Status = status;
            this.UtcDate = DateTime.UtcNow;
        }

        public override String ToString() => this.ToString(Log._DefaultDateFormat);

        public String ToString(String dateFormat)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Host ?? "-");
            sb.Append(' ');
            sb.Append(this.Identifier ?? "-");
            sb.Append(' ');
            sb.Append(this.AuthorizedUser ?? "-");
            sb.Append(" [");
            sb.Append(this.Date.ToString(dateFormat ?? Log._DefaultDateFormat));
            sb.Append("] ");
            sb.Append(this.Request is null ? "-" : $"\"{this._RequestNoWhiteSpace}\"");
            sb.Append(' ');
            sb.Append((int) this.Status);
            sb.Append(' ');
            sb.Append(this.Bytes);
            return sb.ToString();
        }
    }
}