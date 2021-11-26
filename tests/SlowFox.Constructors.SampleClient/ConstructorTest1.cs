
namespace Logic.Readers
{
    using SlowFox;
    using Logic.IO;

    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}

namespace Logic.IO
{
    public interface IDatabase { }
}