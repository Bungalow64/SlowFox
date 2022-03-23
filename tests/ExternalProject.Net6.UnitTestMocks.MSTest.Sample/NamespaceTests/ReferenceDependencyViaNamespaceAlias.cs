using I = ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaNamespaceAlias
    {
        private readonly I.IUserReader _userReader;

        public ReferenceDependencyViaNamespaceAlias(I.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
