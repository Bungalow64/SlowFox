﻿using ExternalProject.Net5.UnitTestMocks.MSTest.Sample.InjectableDependencies;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ThreeDependencies
    {
        private readonly IUserReader _userReader;
        private readonly IUserWriter _userWriter;
        private readonly IUserCache _userCache;

        public ThreeDependencies(IUserReader userReader, IUserWriter userWriter, IUserCache userCache)
        {
            _userReader = userReader;
            _userWriter = userWriter;
            _userCache = userCache;
        }

        public string GetName() => _userReader.GetName();
        public void UpdateName(string name) => _userWriter.UpdateName(name);
        public void ClearCache() => _userCache.ClearCache();
    }
}
