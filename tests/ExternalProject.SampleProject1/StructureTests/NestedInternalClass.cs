namespace ExternalProject.SampleProject1.StructureTests
{
    public partial class NestedInternalClass
    {
        [SlowFox.InjectDependencies(typeof(IDataReader))]
        internal partial class InnerClass
        {
            public IDataReader DataReader => _dataReader;
        }

        public static bool CheckInnerClass(IDataReader dataReader)
        {
            var innerClass = new InnerClass(dataReader);
            return innerClass.DataReader == dataReader;
        }
    }
}
