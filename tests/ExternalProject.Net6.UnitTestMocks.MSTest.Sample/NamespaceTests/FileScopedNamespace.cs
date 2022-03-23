namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests;

public class FileScopedNamespace
{
    private readonly ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies.IUserReader _userReader;

    public FileScopedNamespace(ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

    public string GetName() => _userReader.GetName();
}
