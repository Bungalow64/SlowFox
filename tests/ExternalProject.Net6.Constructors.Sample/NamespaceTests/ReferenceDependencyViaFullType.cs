namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net6.Constructors.Sample.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net6.Constructors.Sample.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
