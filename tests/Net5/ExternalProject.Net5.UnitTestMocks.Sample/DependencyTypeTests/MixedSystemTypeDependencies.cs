using System;

namespace ExternalProject.Net5.UnitTestMocks.Sample.DependencyTypeTests
{
    public class MixedSystemTypeDependencies
    {
        private readonly int _index;
        private readonly string _name;
        private readonly long _longNumber;
        private readonly Int32 _int32Number;
        private readonly Int64 _int64Number;
        private readonly double _doubleNumber;

        public MixedSystemTypeDependencies(int index, string name, long longNumber, Int32 int32Number, Int64 int64Number, double doubleNumber)
        {
            _index = index;
            _name = name;
            _longNumber = longNumber;
            _int32Number = int32Number;
            _int64Number = int64Number;
            _doubleNumber = doubleNumber;
        }

        public int GetIndex() => _index;
        public string GetName() => _name;
        public long GetLongNumber() => _longNumber;
        public Int32 GetInt32Number() => _int32Number;
        public Int64 GetInt64Number() => _int64Number;
        public double GetDoubleNumber() => _doubleNumber;
    }
}