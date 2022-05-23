namespace ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests
{
    public class ReferenceDependencyViaFullType
    {
        private readonly ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies.IUserReader _userReader;

        public ReferenceDependencyViaFullType(ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

        public string GetName() => _userReader.GetName();
    }
}
