using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CateringSystem.Logging;

namespace CateringSystemTest
{
    [TestClass]
    public class TestLog
    {
        private static readonly String[] _MonthStrings = new String[]
        {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"
        };

        [TestMethod]
        public void TestToString_NullFormat()
        {
            Log log = new Log("127.0.0.1", null, "Chris", null, HttpStatusCode.OK, 400);
            String str = log.ToString(null);
            DateTime date = log.Date;
            String dateString = $"{date.Day.ToString().PadLeft(2, '0')}/{TestLog._MonthStrings[date.Month - 1]}/{date.Year} {date.Hour.ToString().PadLeft(2, '0')}:{date.Minute.ToString().PadLeft(2, '0')}:{date.Second.ToString().PadLeft(2, '0')} +01:00";
            Assert.AreEqual($"127.0.0.1 - Chris [{dateString}] - 200 400", str);
        }
    }
}