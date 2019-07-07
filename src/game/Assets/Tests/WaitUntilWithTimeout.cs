using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TeamZ.Assets.Tests
{
    public class WaitUntilWithTimeout : CustomYieldInstruction
    {
        private readonly Func<bool> predicate;
        private readonly TimeSpan duration;
        private readonly float start;

        public override bool keepWaiting => this.predicate.Invoke() && Time.time - this.start < this.duration.TotalSeconds;

        public WaitUntilWithTimeout(Func<bool> predicate, TimeSpan duration)
        {
            this.predicate = predicate;
            this.duration = duration;
            this.start = Time.time;
        }
    }
}
