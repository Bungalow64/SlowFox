﻿namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader2))]
    public partial class DerivedBaseClassUsingNoneInjected : DerivedBaseClassWithNoneInjected
    {
        public IDataReader2 DataReader2 => _dataReader2;
    }
}
