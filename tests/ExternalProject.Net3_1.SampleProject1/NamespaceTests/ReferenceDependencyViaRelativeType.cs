namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaRelativeType
    {
        public InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
