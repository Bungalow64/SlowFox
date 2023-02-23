namespace ExternalProject.Net7.UnitTestMocks.Sample.NamespaceTests;

public class FileScopedNamespace
{
    private readonly ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies.IUserReader _userReader;

    public FileScopedNamespace(ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies.IUserReader userReader) => _userReader = userReader;

    public string GetName() => _userReader.GetName();
}
