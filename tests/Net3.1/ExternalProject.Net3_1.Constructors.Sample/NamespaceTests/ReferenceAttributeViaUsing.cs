using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
        public IUserReader Dependency => _userReader;
    }
}
