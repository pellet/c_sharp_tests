using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
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
        
        //TODO: Get this shit to pass...
        [Fact]
        async Task repeat_task_in_order()
        {
            var scheduler = new TestScheduler();

            var delay = 5;

            var actual = new List<int>();

            var task =
                Observable
                    .Timer(TimeSpan.FromSeconds(delay), scheduler)
                    .FirstOrDefaultAsync()
                    .ToTask();
            
            Observable
                .FromAsync(
                    () =>
                        task)
                .Select(_ => { return delay; })
                .Do(_ => delay--)
                .FirstOrDefaultAsync()
                .Do(j =>
                {
                    this.outputHelper.WriteLine(j.ToString());
                })
                .Repeat()
                .TakeWhile(i => { return 0 < i; })
                .Subscribe(actual.Add);
            
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            await task;
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            await task;
            Assert.Equal(expected: new List<int> {5, 4, 3, 2, 1}, actual: actual);
        }
        
        [Fact]
        void repeat_delayed_observable_result_in_order()
        {
            var scheduler = new TestScheduler();

            var delay = 5;

            var actual = new List<int>();
                
            Observable
                .Defer(
                    () =>
                        Observable
                            .Timer(TimeSpan.FromSeconds(delay), scheduler))
                .Select(_ => { return delay; })
                .Do(_ => delay--)
                .FirstOrDefaultAsync()
                .Do(j =>
                {
                    this.outputHelper.WriteLine(j.ToString());
                })
                .Repeat()
                .TakeWhile(i => { return 0 < i; })
                .Subscribe(actual.Add);
            
            scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
            Assert.Equal(expected: new List<int> {5, 4, 3, 2, 1}, actual: actual);
        }
    }
}