﻿using ExternalProject.Net6.Constructors.Sample.ConfigTests.WithUnderscores;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.ConfigTests.WithUnderscores
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(TestClass))]
    public partial class TestClassTests
    {
        [Test]
        public void HasDependency()
        {
            TestClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}