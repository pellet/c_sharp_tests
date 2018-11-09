using System;
using Optional;
using Xunit;

namespace CSharpTupleTest
{
    public class OptionalTest
    {
        [Fact]
        void option_pattern()
        {
            var option = Option.Some(1).WithException(new Exception());
            option.Match(
                some: i => 2,
                none: e => 4);
        }
    }
}