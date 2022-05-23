# Introduction 
SlowFox is a suite of .NET source generators, aiming to reduce the amount of repetitive code you need to write and maintain.

Source generators incur no run-time cost (as no reflection is involved), because the code is created at build time.  Plus, using a supported IDE (like Visual Studio 2019/2022), you can see what's being generated immediately when you save your source code.

There are currently 2 generators available via SlowFox:
1. Constructors, (this package), for generating constructors and private variables
2. [UnitTestMocks](https://www.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/), for generating mock objects in unit tests

# SlowFox.Constructors

[![](https://img.shields.io/nuget/v/SlowFox.Constructors)](https://www.nuget.org/packages/SlowFox.Constructors/)
[![](https://img.shields.io/nuget/dt/SlowFox.Constructors)](https://www.nuget.org/packages/SlowFox.Constructors/)
[![Build Status](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_apis/build/status/Bungalow64.SlowFox.CI?branchName=refs%2Fpull%2F5%2Fmerge)](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_build/latest?definitionId=17)
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