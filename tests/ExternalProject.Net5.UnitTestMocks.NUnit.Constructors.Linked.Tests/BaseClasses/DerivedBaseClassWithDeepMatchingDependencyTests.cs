﻿using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDeepMatchingDependency))]
    public partial class DerivedBaseClassWithDeepMatchingDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithDeepMatchingDependency model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
