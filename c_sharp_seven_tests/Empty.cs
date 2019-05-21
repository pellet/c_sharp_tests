using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace CSharpTupleTest
{
    public class Empty
    {
        [Fact]
        async Task task_returns_false_if_empty()
        {
            var task =
                Observable
                    .Empty<bool>()
                    .DefaultIfEmpty(false)
                    .ToTask();
            Assert.False(await task);
        }
    }
}