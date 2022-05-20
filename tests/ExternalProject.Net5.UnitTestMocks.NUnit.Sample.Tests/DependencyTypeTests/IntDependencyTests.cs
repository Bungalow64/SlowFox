﻿using NUnit.Framework;
using ExternalProject.Net5.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(IntDependency))]
    public partial class IntDependencyTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            const int index = 1001;
            IntDependency model = Create(index);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            const int index = 1001;
            var model = Create(index);

            Assert.That(model.GetName(), Is.EqualTo("Jamie"));
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);

            Assert.That(model.GetIndex(), Is.EqualTo(index));
        }
    }
}
