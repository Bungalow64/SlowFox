using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaUsing
    {
        private readonly IUserReader _userReader;

        public ReferenceDependencyViaUsing(IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
