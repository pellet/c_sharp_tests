using System;
using System.Reactive;
using System.Threading.Tasks;

namespace CSharpTupleTest
{
    public interface LambdaPropertyInterface
    {
        IObservable<Unit> Ticker { get; }
        Task<bool> Success { get; }
        Func<string, bool> StringSuccess { get; }
    }
}