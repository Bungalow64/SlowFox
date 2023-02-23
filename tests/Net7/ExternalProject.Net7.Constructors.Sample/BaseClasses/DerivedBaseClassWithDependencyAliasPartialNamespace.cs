﻿using ExternalProject.Net7.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net7.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithDependencyAliasPartialNamespace : AbstractBaseClassWithDependencyAliasPartialNamespace
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
