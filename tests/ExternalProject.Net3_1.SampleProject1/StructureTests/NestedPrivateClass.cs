namespace ExternalProject.Net3_1.SampleProject1.StructureTests
{
    public partial class NestedPrivateClass
    {
        [SlowFox.InjectDependencies(typeof(IDataReader))]
        private partial class InnerClass
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
