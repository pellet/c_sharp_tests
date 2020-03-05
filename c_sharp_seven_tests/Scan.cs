
using System;
using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class Scan
    {
        [Fact]
        void scan()
        {
            int? result = null;
            Observable
                .Range(0, 4)
                .Scan((x, y) =>
                {
                    return x + y;
                })
                .Do(res =>
                {
                    result = res;
                })
                .Repeat(2)
                .Subscribe();
            Assert.Equal(expected: 3, actual: result);
        }
    }
}
