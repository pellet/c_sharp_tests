using System;

namespace CSharpTupleTest
{
    public class TupleManager
    {
        public delegate (int x, string y) GetTuple();

        private readonly Lazy<(int x, string y)> tuple = 
            new Lazy<(int x, string y)>(() => 
                (x:1, y:"2"));

        public GetTuple TupleService =>
            () => tuple.Value;
    }
}