using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using TimerUI.AppInit;
using TimerUI.Messages;

namespace TimerUI
{
    public class CustomStopwatch
    {
        private readonly Stopwatch _accurateTimer;
        private readonly DispatcherTimer _timerWithEvenHooks;
        private readonly IEventAggregator _messenger;

        public long Milliseconds { get; set; }

        public CustomStopwatch()
        {
            _accurateTimer = new Stopwatch();
            _timerWithEvenHooks = new DispatcherTimer();
            _timerWithEvenHooks.Tick += OnEachTick;
            _timerWithEvenHooks.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.Container
                                     .GetAllInstances(typeof(IEventAggregator))
                                     .FirstOrDefault() as IEventAggregator;
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Milliseconds = _accurateTimer.ElapsedMilliseconds;
            _messenger.Publish(new StopwatchTickEvent(Milliseconds));
        }

        public void Start()
        {
            _accurateTimer.Start();
            _timerWithEvenHooks.Start();
        }

        public void Stop()
        {
            _accurateTimer.Stop();
            _timerWithEvenHooks.Stop();
        }

        public void Reset()
        {
            _accurateTimer.Restart();
        }
    }
}