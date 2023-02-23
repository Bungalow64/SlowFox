using SlowFox;

namespace ExternalProject.Net7.Constructors.Sample.WithinFolder
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
