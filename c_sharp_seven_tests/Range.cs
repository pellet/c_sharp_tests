

using System;
using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class Range
    {
        [Fact]
        void observable_range_completes_when_finished()
        {
            bool? completed = null;
            Observable
                .Range(0, 3)
                .Finally(() => completed = true)
                .Subscribe();
            Assert.True(completed);
        }
    }
}