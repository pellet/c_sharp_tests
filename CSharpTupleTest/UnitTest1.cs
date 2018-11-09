using System;
using Xunit;

namespace CSharpTupleTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var tupleManager = new TupleManager();
            var tupleService = tupleManager.TupleService();
            Assert.NotNull(tupleService.y);
        }
    }
}