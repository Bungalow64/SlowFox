using ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net7.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaUsing
    {
        private readonly IUserReader _userReader;

        public ReferenceDependencyViaUsing(IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
