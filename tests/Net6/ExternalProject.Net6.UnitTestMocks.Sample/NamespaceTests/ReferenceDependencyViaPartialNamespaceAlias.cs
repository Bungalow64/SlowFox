using I = ExternalProject.Net6.UnitTestMocks.Sample;

namespace ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaPartialNamespaceAlias
    {
        private readonly I.InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaPartialNamespaceAlias(I.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
