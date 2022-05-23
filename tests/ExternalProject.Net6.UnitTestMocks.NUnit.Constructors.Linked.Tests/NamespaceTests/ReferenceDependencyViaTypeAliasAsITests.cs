﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAliasAsI))]
public partial class ReferenceDependencyViaTypeAliasAsITests
{
    [Test]
    public void HasDependency()
    {
        ReferenceDependencyViaTypeAliasAsI model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_i.Object));
    }
}
