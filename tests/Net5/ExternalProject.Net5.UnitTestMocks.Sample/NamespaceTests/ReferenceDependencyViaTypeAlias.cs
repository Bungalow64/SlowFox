using I = ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaTypeAlias
    {
        private readonly I _userReader;

        public ReferenceDependencyViaTypeAlias(I userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
