using SlowFox.UnitTestMocks.MSTest.Tests.Base;
using System.Threading.Tasks;
using Xunit;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using SlowFox.UnitTestMocks.MSTest.Generators;

namespace SlowFox.UnitTestMocks.MSTest.Tests.Generators
{
    public class InjectMocksAttributeGeneratorTests : BaseTest<InjectMocksAttributeGenerator>
    {
        [Fact]
        public async Task AttributeBuilds()
        {
            var generated = @"using System;

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectMocksAttribute : Attribute
    {
        public Type Type { get; set; }
        public InjectMocksAttribute() { }
        public InjectMocksAttribute(Type type) => Type = type;
    }
}";
            await AssertGeneration(generated, "SlowFox.UnitTestMocks.MSTest.Generators.InjectMocksAttribute.Generated.cs");
        }

        [Fact]
        public void AttributeBuilds_WithNoWarnings()
        {
            (_, Compilation output, ImmutableArray<Diagnostic> diagnostics) = RunGenerator(GenericCode);

            Assert.Empty(diagnostics);
            Assert.Empty(output.GetDiagnostics());
        }
    }
}
