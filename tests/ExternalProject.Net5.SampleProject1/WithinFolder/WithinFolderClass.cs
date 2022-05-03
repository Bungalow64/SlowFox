using SlowFox;

namespace ExternalProject.Net5.SampleProject1.WithinFolder
{
    [InjectDependencies(typeof(IDataReader))]
    public partial class WithinFolderClass
    {
        public static WithinFolderClass BuildSelf()
        {
            return new WithinFolderClass(null);
        }
    }

    public interface IDataReader { }
}
