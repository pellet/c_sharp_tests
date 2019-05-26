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
        void repeat_delayed_observable_result_in_order()
        {
            var scheduler = new TestScheduler();

            var delay = 5;
            long? actual = null;
            
            Observable
                .Defer(
                    () =>
                        Observable
                            .Timer(TimeSpan.FromSeconds(delay), scheduler))
                .Select(_ =>
                {
                    return delay;
                })
                .Do(_ => delay--)
                .FirstOrDefaultAsync()
                .Do(j =>
                {
                    this.outputHelper.WriteLine(j.ToString());
                    actual = j;
                })
                .Repeat()
                .TakeWhile(i =>
                {
                    return 0 < i;
                })
                .Subscribe();
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            Assert.Equal(expected: 0, actual: actual);
        }
    }
}