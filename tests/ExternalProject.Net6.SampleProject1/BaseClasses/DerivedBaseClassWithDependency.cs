﻿using ExternalProject.Net6.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net6.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithDependency : AbstractBaseClassWithDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
