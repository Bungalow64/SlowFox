﻿using SlowFox;

namespace ExternalProject.Net6.SampleProject1.WithinFolder
{
    [InjectDependencies(typeof(IDataReader))]
    public partial class Class1
    {
        public static Class1 BuildSelf()
        {
            return new Class1(null);
        }
    }

    public interface IDataReader { }
}