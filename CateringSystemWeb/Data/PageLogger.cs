using System;
using System.Net;

using CateringSystem.Logging;

namespace CateringSystemWeb.Data
{
    public class PageLogger : ILogger
    {
        public String AuthorizedUser { get; set; }
        public String Host { get; set; }
        public String Identifier { get; set; }
        public PageLogger.LogWriter Writer { get; }

        public PageLogger(PageLogger.LogWriter writer) => this.Writer = writer ?? throw new ArgumentNullException(nameof (writer));

        public Log CreateClientLog(String clientEndPoint, String request, HttpStatusCode status, int bytes) => new Log(clientEndPoint, this.Identifier, this.AuthorizedUser, request, status, bytes);
        public void ClientLog(String clientEndPoint, String request, HttpStatusCode status, int bytes) => this.Writer(new Log(clientEndPoint, this.Identifier, this.AuthorizedUser, request, status, bytes).ToString());
        public void Dispose() { }
        public void Log(Log log) => this.Writer(log.ToString());
        public void Log(Log log, String dateFormat) => this.Writer(log.ToString(dateFormat));

        public delegate void LogWriter(String str, params Object[] args);
    }
}