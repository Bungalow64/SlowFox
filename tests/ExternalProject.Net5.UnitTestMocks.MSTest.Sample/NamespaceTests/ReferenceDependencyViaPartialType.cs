﻿namespace ExternalProject.Net5.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaPartialType
    {
        private readonly InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaPartialType(InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
