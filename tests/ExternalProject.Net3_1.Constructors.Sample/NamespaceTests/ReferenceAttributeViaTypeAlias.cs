﻿using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
