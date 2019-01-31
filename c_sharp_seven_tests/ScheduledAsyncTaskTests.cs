using Microsoft.Reactive.Testing;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSharpTupleTest
{
    public class ScheduledAsyncTaskTests
    {
        [Fact]
        public async Task test()
        {
            var scheduler = new TestScheduler();
            var reader = new Reader();
            Observable
                .FromAsync(_ => reader.Read())
                .SubscribeOn(scheduler)
                .Subscribe();
            scheduler.AdvanceBy(1);
        }

        class Reader
        {
            public async Task Read()
            {
                await Observable
                    .Create<byte[]>(
                    o =>
                    {
                        o.OnNext(new byte[] { 0, 1 });
                        return Disposable.Empty;
                    })
                    .ToTask();
            }
        }
    }
}
