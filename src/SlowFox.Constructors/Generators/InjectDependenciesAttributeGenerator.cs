using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SlowFox.Constructors.Generators
{
    [Generator]
    public sealed class InjectDependenciesAttributeGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // nothing happening
        }

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
                "InjectDependenciesAttribute.Generated",
                SourceText.From(code, Encoding.UTF8)
            );
        }
    }
}