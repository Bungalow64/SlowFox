using Microsoft.CodeAnalysis;
using SlowFox.Constructors.Generators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlowFox.Constructors.Tests.Base
{
    public abstract class BaseWithAttributeTest<TGenerator1> : BaseMultiTest<TGenerator1, InjectDependenciesAttributeGenerator>
        where TGenerator1 : ISourceGenerator, new()
    {
        private const string _expectedAttributeFileName = "SlowFox.Constructors.Generators.InjectDependenciesAttribute.Generated.cs";
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
        protected string ExpectedAttributeFileName => _expectedAttributeFileName;
        protected string ExpectedAttributeContents => _expectedAttributeContents;

        protected Task AssertFullGeneration(string generatorOutput, string generatorFilename, params string[] code)
        {
            return AssertMultipleGenerations(new Dictionary<string, string> { { generatorFilename, generatorOutput } }, new Dictionary<string, string> { { _expectedAttributeFileName, _expectedAttributeContents } }, code);
        }
        protected Task AssertGenerationTwoOutputs(string generatorOutput1, string generatorFilename1, string generatorOutput2, string generatorFilename2, params string[] code)
        {
            return AssertMultipleGenerations(new Dictionary<string, string> { { generatorFilename1, generatorOutput1 }, { generatorFilename2, generatorOutput2 } }, new Dictionary<string, string> { { _expectedAttributeFileName, _expectedAttributeContents } }, code);
        }
        protected Task AssertNoGeneration(params string[] code)
        {
            return AssertMultipleGenerations(new Dictionary<string, string>(), new Dictionary<string, string> { { _expectedAttributeFileName, _expectedAttributeContents } }, code);
        }
        protected Task AssertMultipleGenerations(IDictionary<string, string> primaryGeneratorOutputs, params string[] code)
        {
            return AssertMultipleGenerations(primaryGeneratorOutputs, new Dictionary<string, string> { { _expectedAttributeFileName, _expectedAttributeContents } }, code);
        }
    }
}
