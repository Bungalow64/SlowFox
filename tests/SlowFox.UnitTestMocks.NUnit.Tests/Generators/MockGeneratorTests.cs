using Microsoft.CodeAnalysis.Testing;
using SlowFox.UnitTestMocks.NUnit.Generators;
using SlowFox.UnitTestMocks.NUnit.Tests.Base;
using System.Threading.Tasks;
using Xunit;

namespace SlowFox.UnitTestMocks.NUnit.Tests.Generators;

public class MockGeneratorTests : BaseMockTest<MockGenerator>
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [SetUp]
        public void Setup()
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [SetUp]
        public void Setup()
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [SetUp]
        public void Setup()
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [SetUp]
        public void Setup()
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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;

        [SetUp]
        public void Setup()
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
    public async Task Class_WithStaticConstructorAndInstanceConstructor_GenerateMocks()
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
        public static readonly long baseline;

        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;

        static UserReader()
        {
            baseline = System.DateTime.UtcNow.Ticks;
        }

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
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
    public async Task Class_MockingClassWithInjectedDependenciesAndDefinedConstructor_MockConstructorOnly()
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
@"using System;

namespace Logic.IO
{
    [SlowFox.InjectDependencies(typeof(IDatabase2))]
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
    public interface IDatabase2 { }
    public interface IFileReader { }
}
    

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
    public async Task Class_MockingClassWithInjectedDependenciesAndMatchingDefinedConstructor_MockConstructorOnly()
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
@"using System;

namespace Logic.IO
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IFileReader))]
    public partial class UserReader
    {
    }

    public partial class UserReader
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
}
    

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
    public async Task Class_MockingClassWithEmptyInjectedDependenciesAndDefinedConstructor_MockConstructorOnly()
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
@"using System;

namespace Logic.IO
{
    [SlowFox.InjectDependencies]
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
    public interface IDatabase2 { }
    public interface IFileReader { }
}
    

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
    public async Task Class_MockingClassWithInjectedDependencies_MockInjectedDependencies()
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
@"using System;

namespace Logic.IO
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IFileReader))]
    public class UserReader
    {
    }

    public interface IDatabase { }
    public interface IDatabase2 { }
    public interface IFileReader { }
}
    

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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

        var expectedDiagnostic = DiagnosticResult
            .CompilerError("CS1729")
            .WithSpan(@"SlowFox.UnitTestMocks.NUnit\SlowFox.UnitTestMocks.NUnit.Generators.MockGenerator\Logic.Readers.Tests.UserReaderTests.Generated.cs", 20, 24, 20, 43)
            .WithArguments("Logic.IO.UserReader", "2");

        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", expectedDiagnostic, classFile1, classFile2);
    }

    [Fact]
    public async Task Class_MockingClassWithInjectedDependenciesAndEmptyConstructor_MockInjectedDependencies()
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
@"using System;

namespace Logic.IO
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IFileReader))]
    public class UserReader
    {
        public UserReader() { }
    }

    public interface IDatabase { }
    public interface IDatabase2 { }
    public interface IFileReader { }
}

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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

        var expectedDiagnostic = DiagnosticResult
            .CompilerError("CS1729")
            .WithSpan(@"SlowFox.UnitTestMocks.NUnit\SlowFox.UnitTestMocks.NUnit.Generators.MockGenerator\Logic.Readers.Tests.UserReaderTests.Generated.cs", 20, 24, 20, 43)
            .WithArguments("Logic.IO.UserReader", "2");

        await AssertFullGeneration(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", expectedDiagnostic, classFile1, classFile2);
    }

    [Fact]
    public async Task Class_NestedPublicClass_MockInjectedDependencies()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(NestedPublicClass.InnerClass))]
    public partial class InnerClassTests { }
}";

        var classFile2 =
@"using System;

namespace Logic.IO
{
    public partial class NestedPublicClass
    {
        [SlowFox.InjectDependencies(typeof(IDataReader))]
        public partial class InnerClass { }
    }

    public interface IDataReader { }
}

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class InnerClassTests
    {
        private Mock<Logic.IO.IDataReader> _dataReader;

        [SetUp]
        public void Setup()
        {
            _dataReader = new Mock<Logic.IO.IDataReader>(MockBehavior.Strict);
        }

        private Logic.IO.NestedPublicClass.InnerClass Create()
        {
            return new Logic.IO.NestedPublicClass.InnerClass(_dataReader.Object);
        }
    }
}";
        var expectedDiagnostic = DiagnosticResult
            .CompilerError("CS1729")
            .WithSpan(@"SlowFox.UnitTestMocks.NUnit\SlowFox.UnitTestMocks.NUnit.Generators.MockGenerator\Logic.Readers.Tests.InnerClassTests.Generated.cs", 18, 24, 18, 61)
            .WithArguments("Logic.IO.NestedPublicClass.InnerClass", "1");

        await AssertFullGeneration(generated, "Logic.Readers.Tests.InnerClassTests.Generated.cs", expectedDiagnostic, classFile1, classFile2);
    }

    [Fact]
    public async Task Class_WithSkipUnderscoreFalse_GenerateWithUnderscores()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}

namespace Logic.IO
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

        var config =
@"
is_global = true
slowfox_generation.unit_test_mocks.NUnit.skip_underscores = false
";
        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
        await AssertGenerationWithConfig(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", config, classFile1);
    }

    [Fact]
    public async Task Class_WithSkipUnderscoreTrue_GenerateWithoutUnderscores()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}

namespace Logic.IO
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

        var config =
@"
is_global = true
slowfox_generation.unit_test_mocks.NUnit.skip_underscores = true
";
        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> database;
        private Mock<Logic.IO.IFileReader> fileReader;

        [SetUp]
        public void Setup()
        {
            database = new Mock<Logic.IO.IDatabase>(MockBehavior.Strict);
            fileReader = new Mock<Logic.IO.IFileReader>(MockBehavior.Strict);
        }

        private Logic.IO.UserReader Create()
        {
            return new Logic.IO.UserReader(database.Object, fileReader.Object);
        }
    }
}";
        await AssertGenerationWithConfig(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", config, classFile1);
    }

    [Fact]
    public async Task Class_WithUseLooseFalse_GenerateAsStrict()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}

namespace Logic.IO
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

        var config =
@"
is_global = true
slowfox_generation.unit_test_mocks.NUnit.use_loose = false
";
        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
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
        await AssertGenerationWithConfig(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", config, classFile1);
    }

    [Fact]
    public async Task Class_WithUseLooseTrue_GenerateAsLoose()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers.Tests
{
    [InjectMocks(typeof(UserReader))]
    public partial class UserReaderTests { }
}

namespace Logic.IO
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

        var config =
@"
is_global = true
slowfox_generation.unit_test_mocks.NUnit.use_loose = true
";
        var generated =
@"using NUnit.Framework;
using Moq;

namespace Logic.Readers.Tests
{
    public partial class UserReaderTests
    {
        private Mock<Logic.IO.IDatabase> _database;
        private Mock<Logic.IO.IFileReader> _fileReader;

        [SetUp]
        public void Setup()
        {
            _database = new Mock<Logic.IO.IDatabase>(MockBehavior.Loose);
            _fileReader = new Mock<Logic.IO.IFileReader>(MockBehavior.Loose);
        }

        private Logic.IO.UserReader Create()
        {
            return new Logic.IO.UserReader(_database.Object, _fileReader.Object);
        }
    }
}";
        await AssertGenerationWithConfig(generated, "Logic.Readers.Tests.UserReaderTests.Generated.cs", config, classFile1);
    }
}
