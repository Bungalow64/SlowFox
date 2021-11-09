using SlowFox.Constructors.SampleClient.IO;
using System;

namespace SlowFox.Constructors.SampleClient
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IReader))]
    public partial class Class1
    {
        private readonly string name;

        void A()
        {
            //var a = new Class1(null, null);
        }
    }
}
