using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using TimerUI.Messages;

namespace TimerUI
{
    public class StopWatch : TimerUI.Interfaces.IStopWatch
    {
        private readonly DispatcherTimer _timer;
        private readonly IEventAggregator _messenger;

        public int Miliseconds { get; set; }

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnEachTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Miliseconds += 10;
            _messenger.Publish(new StopwatchTickEvent(Miliseconds));
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
