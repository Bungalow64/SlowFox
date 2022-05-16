using ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaUsing
    {
        private readonly IUserReader _userReader;

        public ReferenceDependencyViaUsing(IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
