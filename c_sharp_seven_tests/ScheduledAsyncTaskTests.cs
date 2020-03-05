

using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Xunit;

namespace CSharpTupleTest
{
    public class ScheduledAsyncTaskTests
    {
        [Fact]
        public void test()
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
