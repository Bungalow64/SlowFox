﻿using ExternalProject.Net6.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedNestedInjectedBaseClassWithDependency))]
    public partial class DerivedNestedInjectedBaseClassWithDependencyTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedNestedInjectedBaseClassWithDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
            Assert.AreEqual(_dataReader2.Object, model.DataReader2);
        }
    }
}
