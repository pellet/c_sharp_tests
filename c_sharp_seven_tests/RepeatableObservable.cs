using System;
using System.Reactive.Linq;
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
    }
}