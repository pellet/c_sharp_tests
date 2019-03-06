using System;
using System.Reactive;
using System.Reactive.Linq;
using Xunit;

namespace CSharpTupleTest
{
    public class SubscriptionExceptionSwallowing
    {
        [Fact]
        void exception_not_swallowed_when_thrown_in_subscription()
        {
            var o =
                Observable
                    .Return(Unit.Default);
            Exception actualException = null;
            try
            {
                o
                    .Do(_ => throw new Exception())
                    .Subscribe();
            }
            catch (Exception e)
            {
                actualException = e;
            }
            Assert.NotNull(actualException);
        }
        
        [Fact]
        void exception_swallowed_when_error_handler_in_subscription()
        {
            var o =
                Observable
                    .Return(Unit.Default);
            Exception actualException = null;
            try
            {
                o
                    .Do(_ => throw new Exception())
                    .Subscribe(
                        onNext: _ => {},
                        onError: _ => {});
            }
            catch (Exception e)
            {
                actualException = e;
            }
            Assert.Null(actualException);
        }
    }
}