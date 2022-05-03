namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net3_1.SampleProject1.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net3_1.SampleProject1.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
