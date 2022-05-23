using ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests
{
    public class TwoDependencies
    {
        private readonly IUserReader _userReader;
        private readonly IUserWriter _userWriter;

        public TwoDependencies(IUserReader userReader, IUserWriter userWriter)
        {
            _userReader = userReader;
            _userWriter = userWriter;
        }

        public string GetName() => _userReader.GetName();
        public void UpdateName(string name) => _userWriter.UpdateName(name);
    }
}
