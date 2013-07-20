using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI.ViewModel;

namespace Unit_Tests.ButtonClicked
{
    [TestClass]
    public class StartButtonShould
    {
        [TestMethod]
        public void ChangeButtonTextToStop()
        {
            var mainPageViewModel = new MainPageViewModel();
            var thing = new Object();
            mainPageViewModel.StartCounter(thing);

            var expectedValue = mainPageViewModel.ButtonText;

            Assert.AreEqual(expectedValue, "Stop");
        }
    }
}
