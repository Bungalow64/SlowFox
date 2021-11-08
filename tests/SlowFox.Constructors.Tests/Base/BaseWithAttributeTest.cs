using Microsoft.CodeAnalysis;
using SlowFox.Constructors.Generators;
using System.Threading.Tasks;

namespace SlowFox.Constructors.Tests.Base
{
    public abstract class BaseWithAttributeTest<TGenerator1> : BaseMultiTest<TGenerator1, InjectDependenciesAttributeGenerator>
        where TGenerator1 : ISourceGenerator, new()
    {
        private const string _expectedAttributeFileName = "InjectDependenciesAttribute.Generated.cs";
        private const string _expectedAttributeContents = @"using System;

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
        protected Task AssertFullGeneration(string generatorOutput, string generatorFilename, params string[] code)
        {
            return AssertGeneration(generatorOutput, _expectedAttributeContents, generatorFilename, _expectedAttributeFileName, code);
        }
        protected Task AssertNoGeneration(string code)
        {
            return AssertNoSecondaryGeneration(_expectedAttributeContents, _expectedAttributeFileName, code);
        }
    }
}
