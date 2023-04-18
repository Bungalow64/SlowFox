# Introduction 
SlowFox is a suite of .NET source generators, aiming to reduce the amount of repetitive code you need to write and maintain.

There are currently 2 generators available via SlowFox:
1. **Constructors**, for generating constructors and private variables
2. **UnitTestMocks**, for generating mock objects in unit tests

# SlowFox.Core

[![](https://img.shields.io/nuget/v/SlowFox.Core)](https://www.nuget.org/packages/SlowFox.Core/)
[![](https://img.shields.io/nuget/dt/SlowFox.Core)](https://www.nuget.org/packages/SlowFox.Core/)
[![Build Status](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_apis/build/status/Bungalow64.SlowFox.CI?branchName=main)](https://dev.azure.com/bungalow64/Bungalow64.ConstructorGenerators/_build?definitionId=17&_a=summary)
[![Release Date](https://img.shields.io/github/release-date/bungalow64/slowfox?label=latest%20release)](https://github.com/Bungalow64/SlowFox)
[![Licence](https://img.shields.io/github/license/bungalow64/slowfox)](https://github.com/Bungalow64/SlowFox)

SlowFox.Core is a library that provides common logic across the SlowFox source generators ([SlowFox.Constructors](https://feed.nuget.org/packages/SlowFox.Constructors) and [SlowFox.UnitTestMocks](https://feed.nuget.org/packages/SlowFox.UnitTestMocks.xUnit)).

This package is required as a dependency of the source generators, but doesn't do anything just on its own.

Check out the documentation for the source generators themselves:

- [SlowFox.Constructors](https://feed.nuget.org/packages/SlowFox.Constructors/)
- [SlowFox.UnitTestMocks.xUnit](https://feed.nuget.org/packages/SlowFox.UnitTestMocks.xUnit/)
- [SlowFox.UnitTestMocks.NUnit](https://feed.nuget.org/packages/SlowFox.UnitTestMocks.NUnit/)
- [SlowFox.UnitTestMocks.MSTest](https://feed.nuget.org/packages/SlowFox.UnitTestMocks.MSTest/)