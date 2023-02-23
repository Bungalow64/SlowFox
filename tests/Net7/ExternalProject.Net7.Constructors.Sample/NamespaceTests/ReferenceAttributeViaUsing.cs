using ExternalProject.Net7.Constructors.Sample.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net7.Constructors.Sample.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
        public IUserReader Dependency => _userReader;
    }
}
