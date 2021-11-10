using SlowFox.Constructors.SampleClient.IO;
using System;

namespace SlowFox.Constructors.SampleClient
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IReader))]
    public partial class Class1
    {
        private Class1 inputClass;

        public void A(Class1 input)
        {
            inputClass = input ?? throw new ArgumentNullException(nameof(input));
        }
    }
}
