using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
        public IUserReader Dependency => _userReader;
    }
}
