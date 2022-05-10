﻿using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    public partial class DerivedBaseClassWithNoneInjected : AbstractBaseClassWithInjectedDependency
    {
        private readonly IDataReader _dataReader;

        public DerivedBaseClassWithNoneInjected(IUserReader userReader, IDataReader dataReader) : base(userReader)
        {
            _dataReader = dataReader;
        }

        public IDataReader DataReader => _dataReader;
    }
}
