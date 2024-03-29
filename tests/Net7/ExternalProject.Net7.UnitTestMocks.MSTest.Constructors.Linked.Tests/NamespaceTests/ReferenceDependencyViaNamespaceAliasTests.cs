﻿using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
public partial class ReferenceDependencyViaNamespaceAliasTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaNamespaceAlias model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
