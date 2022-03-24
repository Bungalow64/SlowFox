﻿using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.DependencyTypeTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.Tests.DependencyTypeTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(NonInterfaceDependency))]
    public partial class NonInterfaceDependencyTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            const int index = 1001;
            NonInterfaceDependency model = Create(index);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            const int index = 1001;
            var model = Create(index);

            Assert.AreEqual("Jamie", model.GetName());
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);

            Assert.AreEqual(index, model.GetIndex());
        }
    }
}
