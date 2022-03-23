using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests
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
