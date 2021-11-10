using System;
using SlowFox;

namespace ExternalProject.SampleProject1.Others
{
    [InjectDependencies(typeof(IDataReader2))]
    public partial class Class1a
    {
        public static Class1a BuildSelf()
        {
            return new Class1a(null);
        }
    }

    public interface IDataReader2 { }
}
