using System;
using Xunit;

namespace c_sharp_eight_tests
{
    public class UnitTest1
    {
        [Fact]
        public void non_nullable_warning()
        {
            string s = null; // not ok
            string? x = null; // Ok

        }

        [Fact]
        public void range_test()
        {
            string[] names =
            {
                "Archimedes", "Pythagoras", "Euclid", "Socrates", "Plato"
            };
            Range range = 1..4;
            var i = 0;
            foreach (var name in names[1..4])
            {
                i++;
            }
            Assert.Equal(3, i);
        }
    }
}
