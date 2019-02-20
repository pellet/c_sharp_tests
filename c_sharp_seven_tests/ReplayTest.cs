
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class ReplayTest
    {
        [Fact]
        void test_replay_of_unsubscribed_observable()
        {
            var i = 0;
            var replayObservable =
                Observable
                    .Create<bool>(o =>
                    {
                        if (i++==0)
                        {
                            o.OnNext(false);
                        }
                        else
                        {
                            o.OnNext(true);
                        }
                        return Disposable.Empty;
                    })
                    .Replay(1)
                    .RefCount();

            var res = new List<bool>();
            replayObservable
                .Subscribe(res.Add)
                .Dispose();
            Assert.False(res[0]);
            
            replayObservable
                .Subscribe(res.Add)
                .Dispose();
            Assert.False(res[1]);
            Assert.True(res[2]);
        }
    }
}