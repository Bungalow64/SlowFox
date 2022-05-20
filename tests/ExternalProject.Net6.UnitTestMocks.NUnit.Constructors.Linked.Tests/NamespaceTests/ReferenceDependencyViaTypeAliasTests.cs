﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
public partial class ReferenceDependencyViaTypeAliasTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceDependencyViaTypeAlias model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_reader.Object));
    }
}
