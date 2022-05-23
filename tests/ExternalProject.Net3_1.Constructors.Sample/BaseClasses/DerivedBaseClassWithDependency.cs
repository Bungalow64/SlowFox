﻿using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithDependency : AbstractBaseClassWithDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
