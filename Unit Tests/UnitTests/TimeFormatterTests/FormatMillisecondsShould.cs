using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI.Helpers;

namespace Unit_Tests.UnitTests.TimeFormatterTests
{
    [TestClass]
    public class FormatMillisecondsShould
    {
        private TimeFormatter _timeFormatter;

        [TestInitialize]
        public void SetUp()
        {
            _timeFormatter = new TimeFormatter();
        }

        [TestMethod]
        public void Return0When0Milliseconds()
        {
            var milliseconds = 0;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("0", actualValue);
        }

        [TestMethod]
        public void Return30WhenGiven300Milliseconds()
        {
            var milliseconds = 300;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("300", actualValue);
        }

        [TestMethod]
        public void Return1SecondGiven1000Milliseconds()
        {
            var milliseconds = 1000;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("1:000", actualValue);
        }

        [TestMethod]
        public void Return1Second30MillisecondsGiven1300Milliseconds()
        {
            var milliseconds = 1300;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("1:300", actualValue);
        }

        [TestMethod]
        public void Return1MinGiven60000Milliseconds()
        {
            var milliseconds = 60000;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("1:00:000", actualValue);
        }

        [TestMethod]
        public void Return2Min25Seconds40MillisecondsGiven145400Milliseconds()
        {
            var milliseconds = 145400;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("2:25:400", actualValue);
        }
    }
}
