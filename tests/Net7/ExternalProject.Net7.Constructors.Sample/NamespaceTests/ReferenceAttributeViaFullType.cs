using ExternalProject.Net7.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net7.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaFullType
    {
        public IUserReader Dependency => _userReader;
    }
}
