namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net5.Constructors.Sample.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net5.Constructors.Sample.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
