using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SlowFox.Constructors.Generators
{
    /// <summary>
    /// Source generator for generating the attribute to be used for identifying classes to have a constructor automatically generated
    /// </summary>
    [Generator]
    public sealed class InjectDependenciesAttributeGenerator : ISourceGenerator
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
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";
            context.AddSource(
                "SlowFox.Constructors.Generators.InjectDependenciesAttribute.Generated.cs",
                SourceText.From(code, Encoding.UTF8)
            );
        }
    }
}