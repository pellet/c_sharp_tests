using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_eight_tests
{
    public class CompositeDisposableTests
    {
        [Fact]
        void test_composite_disposable_disposes_for_class()
        {
            void createAndReleaseClass()
            {
                new TestClass(outputHelper);
            }
            
            createAndReleaseClass();
            GC.Collect();
        }
        
        ITestOutputHelper outputHelper;
        
        public CompositeDisposableTests(ITestOutputHelper outputHelper) => 
            this.outputHelper = outputHelper;

        class TestClass : IDisposable
        {
            readonly ITestOutputHelper outputHelper;
            CompositeDisposable disposables = new CompositeDisposable();

            public TestClass(ITestOutputHelper outputHelper)
            {
                this.outputHelper = outputHelper;
                var observable =
                    Observable.Create<int>(
                        o =>
                        {
                            outputHelper.WriteLine("created");
                            return Disposable.Create(
                                () => outputHelper.WriteLine("Released!"));
                        });
                this.disposables.Add(
                        observable.Subscribe());
            }

            ~TestClass()
            {
                this.outputHelper.WriteLine("Dealloced Class.");
                //this.disposables.Dispose();
            }

            public void Dispose()
            {
                this.disposables.Dispose();
            }
        }
    }
}