using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI.ViewModel;
using Moq;

namespace Unit_Tests.ButtonClicked
{
    [TestClass]
    public class StartButtonShould
    {
        [TestMethod]
        public void ChangeButtonTextFromStartToStop()
        {
            //This test will probably change because the logic will probably change but I just wanted
            //to get a unit test in here. Below I'll put a few instructions on how to run these.
            //Basicaly, once you have it pulled down from GitHub, set it as the startup project.
            //Once you do that, you should be able to run it in the emulator and push the little
            //arrow at the bottom and that will run the tests. Funny thing is I found an article written
            //by a Teleriker that I met at Stir Trek. The article is here:
            //http://michaelcrump.net/setting-up-unit-testing-in-windows-phone-7-and-8
            //I'm really excited about this

            var mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.ButtonText = "Start"; 
            var sender = new Object();

            mainPageViewModel.ToggleStartAndStopButton(sender);
            var actualValue = mainPageViewModel.ButtonText;

            Assert.AreEqual("Stop", actualValue);
        }

        [TestMethod]
        public void ChangeButtonTextFromStopToStart()
        {
            var mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.ButtonText = "Stop";
            var sender = new Object();

            mainPageViewModel.ToggleStartAndStopButton(sender);
            var actualValue = mainPageViewModel.ButtonText;

            Assert.AreEqual("Start", actualValue);
        }
    }
}
