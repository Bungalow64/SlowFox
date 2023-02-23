namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
