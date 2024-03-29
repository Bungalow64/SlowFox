﻿using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaNamespaceAlias))]
public partial class ReferenceAttributeViaNamespaceAliasTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceAttributeViaNamespaceAlias model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
