namespace ExternalProject.Net5.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    public class ReferenceDependencyViaFullType
    {
        private readonly ExternalProject.Net5.UnitTestMocks.MSTest.Sample.InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaFullType(ExternalProject.Net5.UnitTestMocks.MSTest.Sample.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
