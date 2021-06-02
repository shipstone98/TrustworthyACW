using System;

namespace CateringSystem.Logging
{
    public interface ILogger : IDisposable
    {
        String AuthorizedUser { get; }
        String Host { get; }
        String Identifier { get; }

        void Log(Log log);
        void Log(Log log, String dateFormat);
    }
}