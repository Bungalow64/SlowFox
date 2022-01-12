using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SlowFox.UnitTestMocks.MSTest.Generators
{
    /// <summary>
    /// Source generator for generating the attribute to be used for identifying classes to have mocks automatically generated
    /// </summary>
    [Generator]
    public sealed class InjectMocksAttributeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            // nothing happening
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"using System;

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
            context.AddSource(
                "SlowFox.UnitTestMocks.MSTest.Generators.InjectMocksAttribute.Generated.cs",
                SourceText.From(code, Encoding.UTF8)
            );
        }
    }
}