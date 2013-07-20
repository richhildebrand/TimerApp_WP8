using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerUI.Interfaces;

namespace Unit_Tests.Fakes
{
    class FakeStopWatch : IStopWatch
    {
        public int Seconds { get; set; }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
