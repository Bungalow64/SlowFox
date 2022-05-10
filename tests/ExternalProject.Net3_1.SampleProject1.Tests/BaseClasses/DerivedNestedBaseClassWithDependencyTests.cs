﻿using ExternalProject.Net3_1.SampleProject1.BaseClasses;
using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.BaseClasses
{
    public class DerivedNestedBaseClassWithDependencyTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedNestedBaseClassWithDependency(new Mock<IUserReader>().Object, new Mock<IUserWriter>().Object, new Mock<IDataReader>().Object, new Mock<IDataReader2>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var dataReader = new Mock<IDataReader>();
            var dataReader2 = new Mock<IDataReader2>();
            var model = new DerivedNestedBaseClassWithDependency(userReader.Object, userWriter.Object, dataReader.Object, dataReader2.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(dataReader.Object, model.DataReader);
            Assert.Equal(dataReader2.Object, model.DataReader2);
        }
    }
}
