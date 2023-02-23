namespace ExternalProject.Net3_1.Constructors.Sample.StructureTests
{
    public partial class NestedProtectedClass
    {
        [SlowFox.InjectDependencies(typeof(IDataReader))]
        protected partial class InnerClass
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
