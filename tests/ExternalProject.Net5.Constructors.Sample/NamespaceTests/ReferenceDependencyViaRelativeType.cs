namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaRelativeType
    {
        public InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
