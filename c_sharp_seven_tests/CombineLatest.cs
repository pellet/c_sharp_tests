
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Xunit;

namespace CSharpTupleTest
{
    public class CombineLatest
    {
        [Fact]
        void CombineSubjects()
        {
            var subby =
                new Subject<bool>();
            var anotherSubby =
                new Subject<int>();

            string actual = null;
            var o =
                Observable
                    .CombineLatest(
                        subby,
                        anotherSubby,
                        (b, i) =>
                        {
                            return "finito";
                        });
            o.Subscribe(
                r => actual = r);
            anotherSubby.OnNext(10);
            subby.OnNext(true);
            Assert.Equal(expected: "finito", actual: actual);
        }
    }
}