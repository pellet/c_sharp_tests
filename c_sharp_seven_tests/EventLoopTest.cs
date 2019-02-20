using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Threading;
using Xunit;

namespace CSharpTupleTest
{
    public class EventLoopTest
    {
        [Fact]
        public void delayed_lambda_does_not_block_scheduler()
        {
            var countdownEvent = new CountdownEvent(1);
            var list = new List<int>();
            var scheduler = new EventLoopScheduler();
            scheduler.Schedule(
                () => list.Add(1));
            scheduler.Schedule(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    list.Add(3);
                    countdownEvent.Signal();
                });
            scheduler.Schedule(
                () => list.Add(2));
            countdownEvent.Wait(timeout: TimeSpan.FromSeconds(2));
            Assert.Equal(
                expected: new List<int>{1, 2, 3},
                actual: list);
            
        }
        
        [Fact]
        public void lambda_does_not_block_scheduler()
        {
            var countdownEvent = new CountdownEvent(1);
            var list = new List<int>();
            var scheduler = new EventLoopScheduler();
            scheduler.Schedule(
                () => list.Add(1));
            scheduler.Schedule(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    list.Add(3);
                    countdownEvent.Signal();
                });
            scheduler.Schedule(
                () => list.Add(2));
            countdownEvent.Wait(timeout: TimeSpan.FromSeconds(2));
            Assert.Equal(
                expected: new List<int>{1, 2, 3},
                actual: list);
            
        }
    }
}