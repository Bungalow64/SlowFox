﻿using ExternalProject.Net3_1.SampleProject1.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassUsingNoneInjected))]
    public partial class DerivedBaseClassUsingNoneInjectedTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassUsingNoneInjected model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_dataReader2.Object, model.DataReader2);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}