using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI;

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
        public void Return30WhenGiven30Milliseconds()
        {
            var milliseconds = 300;
            var actualValue = _timeFormatter.FormatMilliseconds(milliseconds);

            Assert.AreEqual("30", actualValue);
        }
    }
}
