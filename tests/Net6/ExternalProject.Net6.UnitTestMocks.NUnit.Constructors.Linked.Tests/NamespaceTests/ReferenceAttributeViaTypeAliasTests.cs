﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaTypeAlias))]
public partial class ReferenceAttributeViaTypeAliasTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaTypeAlias model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
