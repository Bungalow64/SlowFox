﻿using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithDependencyAlias : AbstractBaseClassWithDependencyAlias
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
