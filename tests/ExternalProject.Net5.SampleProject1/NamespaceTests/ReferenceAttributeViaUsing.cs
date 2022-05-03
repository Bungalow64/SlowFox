using ExternalProject.Net5.SampleProject1.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
        public IUserReader Dependency => _userReader;
    }
}
