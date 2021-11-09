using Moq;
using SlowFox.Constructors.SampleClient.IO;
using System;
using Xunit;

namespace SlowFox.Constructors.SampleClient.Tests
{
    public class UnitTest1
    {
        private Mock<IDatabase> _database = new Mock<IDatabase>(MockBehavior.Strict);
        private Mock<IReader> _reader = new Mock<IReader>(MockBehavior.Strict);

        [Fact]
        public void Test1()
        {
           // Class1 c = new Class1(_database.Object, _reader.Object);
            //Assert.NotNull(c);
        }
    }
}
