﻿using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net5.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithInjectedDependency : AbstractBaseClassWithInjectedDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}