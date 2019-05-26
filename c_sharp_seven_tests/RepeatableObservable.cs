using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Xunit;
using Xunit.Abstractions;

namespace CSharpTupleTest
{
    public class RepeatableObservable
    {
        private ITestOutputHelper outputHelper;

        public RepeatableObservable(ITestOutputHelper outputHelper) => 
            this.outputHelper = outputHelper;
        
        [Fact]
        void repeat_observable()
        {
            var i = -3;
            var repeatMe =
                Observable
                    .Range(1, 4)
                    .Do(j => i++)
                    .Do(j => this.outputHelper.WriteLine(j.ToString()))
                    .Repeat();
            repeatMe
                .TakeWhile(j => i < j)
                .Subscribe();
            Assert.Equal(expected: 2, actual: i);
        }
        
        
        [Fact]
        void repeat_async_in_order()
        {
            var scheduler = new TestScheduler();

            var delay = 5;
            var delayedAsync =
                Observable
                    .Defer(
                        () =>
                        {
                            var observable =
                                Observable
                                    .Timer(TimeSpan.FromSeconds(delay), scheduler);
                            delay--;
                            return observable;
                        })
                    .ToTask();
            long? actual = null;
            var repeatMe =
                Observable
                    .FromAsync(
                    () =>
                    {
                        return delayedAsync;
                    })
                    .Scan((x, y) =>
                    {
                        return x + y;
                    })
                    .Do(j =>
                    {
                        this.outputHelper.WriteLine(j.ToString());
                        actual = j;
                    })
                    .Repeat();
            repeatMe
                .TakeWhile(i =>
                {
                    return 0 < i;
                })
                .Finally(
                    () =>
                    {
                        
                    })
                .Subscribe();
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            Assert.Equal(expected: 2, actual: actual);
        }
    }
}