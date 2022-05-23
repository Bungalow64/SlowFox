using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
