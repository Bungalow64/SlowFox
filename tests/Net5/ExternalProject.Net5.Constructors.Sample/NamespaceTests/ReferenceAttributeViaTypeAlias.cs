using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
