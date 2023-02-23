namespace ExternalProject.Net7.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net7.Constructors.Sample.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net7.Constructors.Sample.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
