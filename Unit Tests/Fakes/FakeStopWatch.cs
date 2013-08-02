using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Fakes
{
    class FakeStopWatch
    {
        public bool StartWasCalled { get; set; }
        public bool StopWasCalled { get; set; }

        public int Seconds { get; set; }

        public void Start()
        {
            StartWasCalled = true;
        }

        public void Stop()
        {
            StopWasCalled = true;
        }
    }
}
