using ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests
{
    public class DuplicateDependency
    {
        private readonly IUserReader _userReader1;
        private readonly IUserReader _userReader2;
        private readonly IUserWriter _userWriter;

        public DuplicateDependency(IUserReader userReader1, IUserReader userReader2, IUserWriter userWriter)
        {
            _userReader1 = userReader1;
            _userReader2 = userReader2;
            _userWriter = userWriter;
        }

        public string GetName1() => _userReader1.GetName();
        public string GetName2() => _userReader2.GetName();
        public void UpdateName(string name) => _userWriter.UpdateName(name);
    }
}
