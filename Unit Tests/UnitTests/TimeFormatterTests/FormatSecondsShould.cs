using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI;

namespace Unit_Tests.UnitTests.TimeFormatterTests
{
    [TestClass]
    public class FormatSecondsShould
    {
        private TimeFormatter _timeFormatter;

        [TestInitialize]
        public void SetUp()
        {
            _timeFormatter = new TimeFormatter();
        }

        [TestMethod]
        public void Return0Given0Seconds()
        {
            var seconds = 0;
            var formattedTime = _timeFormatter.FormatMilliseconds(seconds);

            Assert.AreEqual("0", formattedTime);
        }

        [TestMethod]
        public void Return55Given55Seconds()
        {
            var seconds = 55;
            var formattedTime = _timeFormatter.FormatMilliseconds(seconds);

            Assert.AreEqual("55", formattedTime);
        }

        [TestMethod]
        public void Return1MinGiven60Seconds()
        {
            var seconds = 60;
            var formattedTime = _timeFormatter.FormatMilliseconds(seconds);

            Assert.AreEqual("1:00", formattedTime);
        }

        [TestMethod]
        public void Return1Min30SecondsGiven90Seconds()
        {
            var seconds = 90;
            var formattedTime = _timeFormatter.FormatMilliseconds(seconds);

            Assert.AreEqual("1:30", formattedTime);
        }
    }
}
