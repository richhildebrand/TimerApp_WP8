using System;
using System.Linq;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerUI.ViewModels;
using Unit_Tests.Fakes;

namespace Unit_Tests.ButtonClicked
{
    [TestClass]
    public class StartButtonShould
    {
        private FakeStopWatch _fakeStopWatch;
        private INavigationService _navigationService;
        private MainPageViewModel _mainPageViewModel;
        private object _sender;

        [TestInitialize]
        public void SetUp()
        {
            _fakeStopWatch = new FakeStopWatch();
            _mainPageViewModel = new MainPageViewModel(_navigationService);
            _sender = new Object();
        }

        //[TestMethod]
        //public void ChangeButtonTextFromStartToStop()
        //{
        //    //This test will probably change because the logic will probably change but I just wanted
        //    //to get a unit test in here. Below I'll put a few instructions on how to run these.
        //    //Basicaly, once you have it pulled down from GitHub, set it as the startup project.
        //    //Once you do that, you should be able to run it in the emulator and push the little
        //    //arrow at the bottom and that will run the tests. Funny thing is I found an article written
        //    //by a Teleriker that I met at Stir Trek. The article is here:
        //    //http://michaelcrump.net/setting-up-unit-testing-in-windows-phone-7-and-8
        //    //I'm really excited about this


        //    // Yes Yes Yes! Go you man!
        //    _mainPageViewModel.ButtonText = "Start";

        //    _mainPageViewModel.ToggleStartAndStopButton(_sender);
        //    var actualValue = _mainPageViewModel.ButtonText;

        //    Assert.AreEqual("Stop", actualValue);
        //}

        //[TestMethod]
        //public void ChangeButtonTextFromStopToStart()
        //{
        //    _mainPageViewModel.ButtonText = "Stop";

        //    _mainPageViewModel.ToggleStartAndStopButton(_sender);
        //    var actualValue = _mainPageViewModel.ButtonText;

        //    Assert.AreEqual("Start", actualValue);
        //}

        //// I hate this but couldn't get mocking to work.
        //// The internets told me the silverlight version of MoQ would work, I could not get it though.
        //[TestMethod]
        //public void CallStopOnTheStopWatchWhenTextIsStop()
        //{
        //    _fakeStopWatch.StopWasCalled = false;
        //    _mainPageViewModel.ButtonText = "Stop";

        //    _mainPageViewModel.ToggleStartAndStopButton(_sender);

        //    Assert.IsTrue(_fakeStopWatch.StopWasCalled);
        //}

        //// I hate this but couldn't get mocking to work.
        //// The internets told me the silverlight version of MoQ would work, I could not get it though.
        //[TestMethod]
        //public void CallStartOnTheStopWatStart()
        //{
        //    _fakeStopWatch.StartWasCalled = false;
        //    _mainPageViewModel.ButtonText = "Start";

        //    _mainPageViewModel.ToggleStartAndStopButton(_sender);

        //    Assert.IsTrue(_fakeStopWatch.StartWasCalled);
        //}
    }
}
