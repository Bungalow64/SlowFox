using System;
using SlowFox;

namespace ExternalProject.SampleProject1.Others
{
    [InjectDependencies(typeof(IDataReader2))]
    public partial class Class1
    {
        public static Class1 BuildSelf()
        {
            return new Class1(null);
        }
    }

    public interface IDataReader2 { }
}
