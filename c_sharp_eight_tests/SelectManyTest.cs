using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace c_sharp_eight_tests
{
    public class SelectManyTest
    {
        [Fact]
        async Task select_many_can_take_only_first_sequence()
        {
            var a =
                new[] {true, false, false}
                    .ToObservable();

            var b =
                new[]
                    {
                        new[] {true}, 
                        new[] {false}, 
                        new[]{false}
                    }
                    .ToObservable();

            var result =
                await a
                .SelectMany(_ => b)
                .FirstOrDefaultAsync();
            
            Assert.Equal(new[] {true}, result);
        }
    }
}