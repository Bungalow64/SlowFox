namespace ExternalProject.Net5.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaFullType
    {
        private readonly ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaFullType(ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
