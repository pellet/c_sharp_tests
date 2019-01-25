using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Xunit;

namespace CSharpTupleTest
{
    public class MulticastTest
    {
        [Fact]
        void test_multicast()
        {
            var @int =
                new Subject<int>();

            const int first = 1;
            var multicast =
                @int
                    .Multicast(new BehaviorSubject<int>(first));
            
            var list = new List<int>();
            multicast
                .Subscribe(list.Add);

            multicast.Connect();
            
            const int second = 2;
            @int.OnNext(second);
            
            Assert.Equal(
                expected: first,
                actual: list.First());
            
            Assert.Equal(
                expected: second,
                actual: list[1]);
        }
    }
}