﻿using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.InjectableDependencies;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.DependencyTypeTests;

public class IntDependency
{
    private readonly IUserReader _userReader;
    private readonly int _index;

    public IntDependency(IUserReader userReader, int index)
    {
        _userReader = userReader;
        _index = index;
    }

    public string GetName() => _userReader.GetName();
    public int GetIndex() => _index;
}
