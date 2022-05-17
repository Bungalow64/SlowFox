﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaNamespaceAlias))]
public partial class ReferenceAttributeViaNamespaceAliasTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaNamespaceAlias model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
