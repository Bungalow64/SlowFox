﻿using ExternalProject.Net7.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
public partial class ReferenceAttributeViaUsingTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaUsing model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
