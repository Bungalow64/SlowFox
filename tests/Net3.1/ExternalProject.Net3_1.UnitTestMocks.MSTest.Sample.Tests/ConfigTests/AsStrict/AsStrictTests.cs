using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.ConfigTests.AsStrict
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestClass]
    public partial class AsStrictTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            Action act = () => Create().GetName();
            Assert.ThrowsException<Moq.MockException>(act);
        }
    }
}