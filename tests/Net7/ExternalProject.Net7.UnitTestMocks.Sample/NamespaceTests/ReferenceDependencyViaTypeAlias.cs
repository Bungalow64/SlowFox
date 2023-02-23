using I = ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net7.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaTypeAlias
    {
        private readonly I _userReader;

        public ReferenceDependencyViaTypeAlias(I userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
