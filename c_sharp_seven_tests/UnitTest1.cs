using Xunit;

namespace CSharpTupleTest
{
    public class TupleTests
    {
        [Fact]
        public void tuple_test()
        {
            var tupleManager = new TupleManager();
            var tupleService = tupleManager.TupleService();
            Assert.NotNull(tupleService.y);
        }
    }
}