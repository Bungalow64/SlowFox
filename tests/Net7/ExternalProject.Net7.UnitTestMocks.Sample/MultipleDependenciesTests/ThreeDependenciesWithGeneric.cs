using ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net7.UnitTestMocks.Sample.MultipleDependenciesTests
{
    public class ThreeDependenciesWithGeneric
    {
        private readonly IUserReader _userReader;
        private readonly IUserWriter _userWriter;
        private readonly ILogger<IUserCache> _logger;

        public ThreeDependenciesWithGeneric(IUserReader userReader, IUserWriter userWriter, ILogger<IUserCache> logger)
        {
            _userReader = userReader;
            _userWriter = userWriter;
            _logger = logger;
        }

        public string GetName() => _userReader.GetName();
        public void UpdateName(string name) => _userWriter.UpdateName(name);
        public IUserCache Log() => _logger.Log();
    }
}
