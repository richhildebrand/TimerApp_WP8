using System;

namespace TimerUI.Interfaces
{
    public interface IStopWatch
    {
        int Miliseconds { get; set; }
        void Start();
        void Stop();
    }
}
