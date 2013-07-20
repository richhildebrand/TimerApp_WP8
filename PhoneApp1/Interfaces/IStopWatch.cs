using System;

namespace TimerUI.Interfaces
{
    public interface IStopWatch
    {
        int Seconds { get; set; }
        void Start();
        void Stop();
    }
}
