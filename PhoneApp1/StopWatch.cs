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

        public int Seconds { get; set; }

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnEachTick;
            _timer.Interval = new TimeSpan(0, 0, 1);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Seconds += 1;
            _messenger.Publish(new StopwatchTickEvent(Seconds));
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
