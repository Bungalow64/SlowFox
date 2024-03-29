﻿using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
public partial class ReferenceDependencyViaFullTypeTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaFullType model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
