using SlowFox;

namespace ExternalProject.Net3_1.SampleProject1.WithinFolder
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
