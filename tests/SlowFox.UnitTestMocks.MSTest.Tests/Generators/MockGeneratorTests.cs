using SlowFox.UnitTestMocks.MSTest.Generators;
using SlowFox.UnitTestMocks.MSTest.Tests.Base;
using System.Threading.Tasks;
using Xunit;

namespace SlowFox.UnitTestMocks.MSTest.Tests.Generators;

public class MockGeneratorTests : BaseWithAttributeTest<MockGenerator>
{
    [Fact]
    public async Task Class_WithAttribute_NoTypes_NothingCreated()
    {
        var classFile =
@"using SlowFox;

namespace Logic.Readers.Tests
{
    [InjectMocks]
    public partial class UserReaderTests { }
}";

        await AssertNoGeneration(classFile);
    }

    [Fact]
    public async Task Class_WithAttribute_GenerateMembers()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;

        public UserReader(IDatabase database, IFileReader fileReader)
        {
            _database = database;
            _fileReader = fileReader;
        }
    }

    public interface IDatabase { }
    public interface IFileReader { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
            _fileReader = new Mock<Logic.IO.IFileReader>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create()
        {
            return new Logic.IO.UserReader(_database.Object, _fileReader.Object);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_ValueTypeDependencies_AddValuesToCreateMethod()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly int _index;
        private readonly string _name;

        public UserReader(IDatabase database, int index, string name)
        {
            _database = database;
            _index = index;
            _name = name;
        }
    }

    public interface IDatabase { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create(int index, string name)
        {
            return new Logic.IO.UserReader(_database.Object, index, name);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_ExcludeOneType_DoNotMockType()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    [ExcludeMocks(typeof(IFileReader))]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;

        public UserReader(IDatabase database, IFileReader fileReader)
        {
            _database = database;
            _fileReader = fileReader;
        }
    }

    public interface IDatabase { }
    public interface IFileReader { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create(Logic.IO.IFileReader fileReader)
        {
            return new Logic.IO.UserReader(_database.Object, fileReader);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_ExcludeTwoTypes_DoNotMockTypes()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    [ExcludeMocks(typeof(IFileReader), typeof(INotificationHandler))]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;
        private readonly INotificationHandler _notificationHandler;

        public UserReader(IDatabase database, IFileReader fileReader, INotificationHandler notificationHandler)
        {
            _database = database;
            _fileReader = fileReader;
            _notificationHandler = notificationHandler;
        }
    }

    public interface IDatabase { }
    public interface IFileReader { }
    public interface INotificationHandler { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create(Logic.IO.IFileReader fileReader, Logic.IO.INotificationHandler notificationHandler)
        {
            return new Logic.IO.UserReader(_database.Object, fileReader, notificationHandler);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_ExcludeTwoTypesViaArray_DoNotMockTypes()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    [ExcludeMocks(new System.Type[] { typeof(IFileReader), typeof(INotificationHandler) })]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;
        private readonly INotificationHandler _notificationHandler;

        public UserReader(IDatabase database, IFileReader fileReader, INotificationHandler notificationHandler)
        {
            _database = database;
            _fileReader = fileReader;
            _notificationHandler = notificationHandler;
        }
    }

    public interface IDatabase { }
    public interface IFileReader { }
    public interface INotificationHandler { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create(Logic.IO.IFileReader fileReader, Logic.IO.INotificationHandler notificationHandler)
        {
            return new Logic.IO.UserReader(_database.Object, fileReader, notificationHandler);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_ExcludeTwoTypesViaImplicitArray_DoNotMockTypes()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    [ExcludeMocks(new[] { typeof(IFileReader), typeof(INotificationHandler) })]
    public partial class UserReaderTests { }
}";

        var classFile2 =
@"namespace Logic.IO
{
    public class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;
        private readonly INotificationHandler _notificationHandler;

        public UserReader(IDatabase database, IFileReader fileReader, INotificationHandler notificationHandler)
        {
            _database = database;
            _fileReader = fileReader;
            _notificationHandler = notificationHandler;
        }
    }

    public interface IDatabase { }
    public interface IFileReader { }
    public interface INotificationHandler { }
}";

        var generated =
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create(Logic.IO.IFileReader fileReader, Logic.IO.INotificationHandler notificationHandler)
        {
            return new Logic.IO.UserReader(_database.Object, fileReader, notificationHandler);
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", classFile1, classFile2);
    }
}
