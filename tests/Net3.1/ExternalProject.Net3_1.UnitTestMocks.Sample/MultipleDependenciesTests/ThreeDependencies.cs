using ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.UnitTestMocks.Sample.MultipleDependenciesTests
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