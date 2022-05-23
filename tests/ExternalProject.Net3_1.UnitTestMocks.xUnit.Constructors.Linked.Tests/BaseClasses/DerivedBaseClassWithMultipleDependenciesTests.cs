﻿using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithMultipleDependencies))]
    public partial class DerivedBaseClassWithMultipleDependenciesTestse
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithMultipleDependencies model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
            Assert.Equal(_dataReader.Object, model.DataReader);
        }
    }
}
