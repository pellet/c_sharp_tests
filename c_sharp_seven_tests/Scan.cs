
using System;
using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class Scan
    {
        [Fact]
        void iterative_loop_using_scan()
        {
            Observable
                .Start(() => 0)
                .Scan((x, y) =>
                {
                    return x + 1;
                })
                .Repeat()
                .TakeWhile(index => index < 3)
                .Subscribe();
            
        }
    }
}