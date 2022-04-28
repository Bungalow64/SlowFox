using I = ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaTypeAlias
    {
        private readonly I _userReader;

        public ReferenceDependencyViaTypeAlias(I userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
