using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using TimerUI.Messages;

namespace TimerUI
{
    public class StopWatch
    {
        private readonly Stopwatch _accurateTimer;
        private readonly DispatcherTimer _timerWithEvenHooks;
        private readonly IEventAggregator _messenger;

        public long Miliseconds { get; set; }

        public StopWatch()
        {
            _accurateTimer = new Stopwatch();
            _timerWithEvenHooks = new DispatcherTimer();
            _timerWithEvenHooks.Tick += OnEachTick;
            _timerWithEvenHooks.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Miliseconds = _accurateTimer.ElapsedMilliseconds;
            _messenger.Publish(new StopwatchTickEvent(Miliseconds));
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
    }
}
