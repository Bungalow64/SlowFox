﻿using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
public partial class ReferenceAttributeViaUsingTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceAttributeViaUsing model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
