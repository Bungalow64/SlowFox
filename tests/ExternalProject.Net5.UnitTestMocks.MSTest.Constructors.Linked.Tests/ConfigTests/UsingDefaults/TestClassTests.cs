﻿using ExternalProject.Net5.Constructors.Sample.ConfigTests.UsingDefaults;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.ConfigTests.UsingDefaults
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(TestClass))]
    public partial class TestClassTests
    {
        [TestMethod]
        public void HasDependency()
        {
            TestClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}