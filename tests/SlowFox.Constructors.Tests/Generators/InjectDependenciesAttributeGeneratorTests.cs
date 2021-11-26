using SlowFox.Constructors.Generators;
using SlowFox.Constructors.Tests.Base;
using System.Threading.Tasks;
using Xunit;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace SlowFox.Constructors.Tests.Generators
{
    public class InjectDependenciesAttributeGeneratorTests : BaseTest<InjectDependenciesAttributeGenerator>
    {
        [Fact]
        public async Task AttributeBuilds()
        {
            var generated = @"using System;

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";
            await AssertGeneration(generated, "InjectDependenciesAttribute.Generated.cs");
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
