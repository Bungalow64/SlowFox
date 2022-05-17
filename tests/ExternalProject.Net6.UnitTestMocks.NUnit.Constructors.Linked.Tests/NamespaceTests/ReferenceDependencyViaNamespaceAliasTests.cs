﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
public partial class ReferenceDependencyViaNamespaceAliasTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceDependencyViaNamespaceAlias model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
