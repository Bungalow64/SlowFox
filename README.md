# Introduction 
SlowFox is a suite of .NET source generators, aiming to reduce the amount of repetitive code you need to write and maintain.

Source generators incur no run-time cost (as no reflection is involved), because the code is created at build time.  Plus, using a supported IDE (like Visual Studio 2019/2022), you can see what's being generated immediately when you save your source code.

There are currently 2 generators available via SlowFox:
1. [Constructors](#slowfoxconstructors), for generating constructors and private variables
2. [UnitTestMocks](#slowfoxunittestmocks), for generating mock objects in unit tests

# SlowFox.Constructors

[![](https://img.shields.io/nuget/v/SlowFox.Constructors)](https://www.nuget.org/packages/SlowFox.Constructors/)
[![](https://img.shields.io/nuget/dt/SlowFox.Constructors)](https://www.nuget.org/packages/SlowFox.Constructors/)
[![Build Status](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_apis/build/status/Bungalow64.SlowFox.CI?branchName=main)](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_build?definitionId=17&_a=summary)
[![Release Date](https://img.shields.io/github/release-date/bungalow64/slowfox?label=latest%20release)](https://github.com/Bungalow64/SlowFox)
[![Licence](https://img.shields.io/github/license/bungalow64/slowfox)](https://github.com/Bungalow64/SlowFox)

SloxFox.Constructors is a generator that allows you to define the injectable dependencies for any given class, and the private class members, constructor and constructor assignments are all automatically created for you.

### How do I get it working?

First off, install the NuGet package into your project:

[![](https://img.shields.io/nuget/v/SlowFox.Constructors)](https://www.nuget.org/packages/SlowFox.Constructors/)

Then, find a class where you want to have the dependencies injected, mark it as `partial`, and add the `SlowFox.InjectDependencies` attribute.  Pass into this attribute the types you want to be injected:

```csharp
namespace MySampleProject
{
    [SlowFox.InjectDependencies(typeof(IUserReader), typeof(IFileHandler))]
    public partial class MyNewClass
    {
    }
}
```

SlowFox will then generate everything needed for these dependencies, as another partial class:

```csharp
namespace MySampleProject
{
    public partial class MyNewClass
    {
        private readonly IUserReader _userReader;
        private readonly IFileHandler _fileHandler;

        public MyNewClass(IUserReader userReader, IFileHandler fileHandler)
        {
            _userReader = userReader;
            _fileHandler = fileHandler;
        }
    }
}
```

You can reference these private members in your original class, and you can call this constructor during DI, or call it manually, or use it in unit tests - it's as if you've written it all yourself:

```csharp
namespace MySampleProject
{
    [SlowFox.InjectDependencies(typeof(IUserReader), typeof(IFileHandler))]
    public partial class MyNewClass
    {
        public void SaveUserImage()
        {
            var image = _userReader.GetImage();
            _fileHandler.AddFile(image);
        }
    }
}
```

This generator is compatible with:
- inheritance
- abstract classes
- generic types
- nullable types
- tuples


### Configuration

Configuration is set in a .editorconfig file.

To configure the generated code to not use underscores for member names, set the `skip_underscores` value to true:

```
[*.cs]
slowfox_generation.constructors.skip_underscores = true
```

To include a null check (and a throw of `ArgumentNullException` if the constructor parameter is null), set `include_nullcheck` to true:

```
[*.cs]
slowfox_generation.constructors.include_nullcheck = true
```

# SlowFox.UnitTestMocks

[![](https://img.shields.io/nuget/v/SlowFox.UnitTestMocks.xUnit)](https://www.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/)
[![](https://img.shields.io/nuget/dt/SlowFox.UnitTestMocks.xUnit)](https://www.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/)
[![Build Status](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_apis/build/status/Bungalow64.SlowFox.CI?branchName=main)](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_build?definitionId=17&_a=summary)
[![Release Date](https://img.shields.io/github/release-date/bungalow64/slowfox?label=latest%20release)](https://github.com/Bungalow64/SlowFox)
[![Licence](https://img.shields.io/github/license/bungalow64/slowfox)](https://github.com/Bungalow64/SlowFox)

SlowFox.UnitTestMocks is a generator that creates mock objects (using [Moq](https://www.nuget.org/packages/Moq/)) for the dependencies of a class that is to be tested.

There are packages that are designed for [xUnit](https://www.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/), [NUnit](https://www.nuget.org/packages/SlowFox.UnitTestMocks.NUnit/) and [MSTest2](https://www.nuget.org/packages/SlowFox.UnitTestMocks.MSTest/).

### How do I get it working?

Firstly, choose and install the NuGet package relating to your testing framework:

| Framework | Package |
| --------- | ------- |
| xUnit     | [![](https://img.shields.io/nuget/v/SlowFox.UnitTestMocks.xUnit)](https://www.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/) |
| NUnit     | [![](https://img.shields.io/nuget/v/SlowFox.UnitTestMocks.NUnit)](https://www.nuget.org/packages/SlowFox.UnitTestMocks.NUnit/) |
| MSTest2   | [![](https://img.shields.io/nuget/v/SlowFox.UnitTestMocks.MSTest)](https://www.nuget.org/packages/SlowFox.UnitTestMocks.MSTest/) |

Next, create a new test class, mark it as `partial` and apply the `InjectMocks` attribute, indicating the class that you're going to be tested:

```csharp
namespace MySampleProject
{
    [SlowFox.InjectMocks(typeof(UserHandler))]
    public partial class UserHandlerTests
    {
    }
}
```

SlowFox will then generate mock objects for each dependency of the selected class, and provide a `Create` method that instantiates a new instance of the selected class with the mock objects used as dependencies:

```csharp
namespace MySampleProject
{
    public partial class UserHandlerTests
    {
        private Mock<IDatabase> _database;
        private Mock<ILogger> _logger;

        public UserHandlerTests()
        {
            _database = new Mock<IDatabase>(MockBehavior.Strict);
            _logger = new Mock<ILogger>(MockBehavior.Strict);
        }

        private UserHandler Create()
        {
            return new UserHandler(_database.Object, _logger.Object);
        }
    }
}
```

> For NUnit and MSTest, the mocks are instantiated in a `Setup` and `Init` method respectively, instead of in a constructor

You can call `Create()` in your tests to get the object to test, and you can reference the mock objects to set up any pre-defined responses, or to perform validation:

```csharp
namespace MySampleProject
{
    [SlowFox.InjectMocks(typeof(UserHandler))]
    public partial class UserHandlerTests
    {
        [Fact]
        public void VerifyAddUser()
        {
            _database
                .Setup(p => p.Save(It.IsAny<User>()));

            UserHandler reader = Create();

            reader.CreateNewUser();

            _database
                .Verify(p => p.Save(It.IsAny<User>()), Times.Once);
        }
    }
}
```

You are able to exclude specific types from being mocked, by using the `ExcludeMocks` attribute.  Any type specified within this attribute will be added as a parameter on the `Create` method, so you can provide a value from within your test:

```csharp
namespace MySampleProject
{
    [SlowFox.InjectMocks(typeof(UserHandler))]
    [SlowFox.ExcludeMocks(typeof(ILogger))]
    public partial class UserHandlerTests
    {
        [Fact]
        public void VerifyAddUser()
        {
            _database
                .Setup(p => p.Save(It.IsAny<User>()));

            ILogger testLogger = BuildTestLogger();

            UserHandler reader = Create(testLogger);

            reader.CreateNewUser();

            _database
                .Verify(p => p.Save(It.IsAny<User>()), Times.Once);
        }
    }
}
```

> Note that types that cannot be mocked (e.g., a static or sealed type) will automatically be excluded from being mocked, and will be treated in the same way as types specified in the `ExcludeMocks` attribute

This generator is compatible with constructors that have been generated using [SlowFox.Constructors](#slowfoxconstructors).

### Configuration

Configuration is set in a .editorconfig file.

To configure the generated code to not use underscores for member names, set the `skip_underscores` value to true:

```
[*.cs]
slowfox_generation.unit_test_mocks.xunit.skip_underscores = true
```

To create the mocks using the `Loose` behaviour (instead of the default of `Strict`), set the `use_loose` value to be true:

```
[*.cs]
slowfox_generation.unit_test_mocks.xunit.use_loose = true
```

> These configuration keys are different for NUnit and MSTest.  For NUnit the configuration prefix is `slowfox_generation.unit_test_mocks.nunit` and for MSTest the prefix is `slowfox_generation.unit_test_mocks.mstest`

# Can I help fix any bugs with this, or add new capabilities?

Of course!  Head on over to the [issues](https://github.com/Bungalow64/SlowFox/issues) page, raise it, and we'll have a chat about what needs to be done.