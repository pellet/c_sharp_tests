using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Xunit;

namespace CSharpTupleTest
{
    public class MockMethodWhichReturnsObservables
    {
        class Sut : IDisposable
        {
            public Func<IObservable<int>> NewObservable { get; set; }
            public Func<IDisposable> Disposables { get; set; }

            public Sut()
            {
                this.NewObservable =
                    () => Observable.Return(1);
                this.Disposables =
                    () => Disposable.Empty;
            }

            public void Dispose() => this.Disposables().Dispose();
        }

        [Fact]
        void test1()
        {
            var ints1 = new Subject<int>();
            var ints2 = new Subject<int>();
            var ints3 = new Subject<int>();
            
            IEnumerable<ISubject<int>> subjects = new []{ints1, ints2, ints3};
            var disposables = new CompositeDisposable();
            var enumerator = subjects.GetEnumerator();
            disposables.Add(enumerator);
            using (var sut = new Sut
            {
                NewObservable = 
                    () =>
                    {
                        enumerator.MoveNext();
                        return enumerator.Current;
                    },
                Disposables = () => disposables
            })
            {
                var expected1 = 2;
                var res1 = sut.NewObservable();
                int? actual1 = null;
                res1.Subscribe(x => actual1 = x);
                ints1.OnNext(expected1);
                Assert.Equal(expected1, actual1.Value);
                var res2 = sut.NewObservable();
                
            }
        }
    }
}