using System.Collections;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CSharpTupleTest
{
    public class EnumerableToObservable
    {
        IEnumerable<bool> b =
            new [] {true, false, false};
        [Fact]
        async Task a()
        {
            var o = b.ToObservable(ImmediateScheduler.Instance);
            var last =
                await o
                    .LastOrDefaultAsync();
            Assert.False(last);
        }

        [Fact]
        async Task c()
        {
            var last = await
                Observable
                    .Return(1)
                    .Concat(Observable.Never<int>())
                    .Select(_ => b.ToObservable())
                    .FirstOrDefaultAsync()
                    .Switch()
                    .LastOrDefaultAsync();
            Assert.False(last);
        }
    }
}