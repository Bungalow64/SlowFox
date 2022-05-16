﻿using ExternalProject.Net3_1.UnitTestMocks.Sample.DependencyTypeTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.Tests.DependencyTypeTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(LocalClassDependency))]
    public partial class LocalClassDependencyTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            LocalClassDependency model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_localValue);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            _localValue
                .Setup(p => p.Name)
                .Returns("Jamie");

            LocalClassDependency model = Create();

            Assert.AreEqual("Jamie", model.GetLocalClass().Name);
        }
    }
}