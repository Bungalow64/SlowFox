﻿using I = ExternalProject.Net5.UnitTestMocks.MSTest.Sample;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaPartialNamespaceAlias
    {
        private readonly I.InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaPartialNamespaceAlias(I.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}