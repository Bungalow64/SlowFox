using I = ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaNamespaceAlias
    {
        private readonly I.IUserReader _userReader;

        public ReferenceDependencyViaNamespaceAlias(I.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
