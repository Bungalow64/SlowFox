﻿using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
    public partial class ReferenceDependencyViaFullTypeTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaFullType model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
        }
    }
}