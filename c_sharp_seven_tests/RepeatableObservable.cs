using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class RepeatableObservable
    {
        [Fact]
        void repeat_observable()
        {
            
            var repeatMe =
                Observable
                    .Range(0, 4)
                    .
        }
    }
}