using SlowFox.Constructors.SampleClient.IO;
using System;

namespace SlowFox.Constructors.SampleClient
{
    [InjectDependencies(typeof(IDatabase), typeof(IReader))]
    public partial class Class1
    {
        void A()
        {
            var a = new Class1(null, null);
        }
    }
}
