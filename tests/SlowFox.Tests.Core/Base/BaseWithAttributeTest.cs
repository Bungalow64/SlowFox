using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlowFox.Tests.Core.Base
{
    public abstract class BaseWithAttributeTest<TGenerator1> : BaseTest<TGenerator1>
        where TGenerator1 : ISourceGenerator, new()
    {
        protected abstract string ExpectedAttributeContents { get; }

        protected Task AssertFullGeneration(string generatorOutput, string generatorFilename, params string[] code)
        {
            return AssertGeneration(new Dictionary<string, string> { { generatorFilename, generatorOutput } }, AppendAttribute(code));
        }
        protected Task AssertFullGeneration(string generatorOutput, string generatorFilename, DiagnosticResult expectedDiagnostic, params string[] code)
        {
            return AssertGeneration(new Dictionary<string, string> { { generatorFilename, generatorOutput } }, new DiagnosticResult[] { expectedDiagnostic }, AppendAttribute(code));
        }
        protected Task AssertFullGeneration(string generatorOutput, string generatorFilename, DiagnosticResult[] expectedDiagnostics, params string[] code)
        {
            return AssertGeneration(new Dictionary<string, string> { { generatorFilename, generatorOutput } }, expectedDiagnostics, AppendAttribute(code));
        }
        protected Task AssertGenerationTwoOutputs(string generatorOutput1, string generatorFilename1, string generatorOutput2, string generatorFilename2, params string[] code)
        {
            return AssertGeneration(new Dictionary<string, string> { { generatorFilename1, generatorOutput1 }, { generatorFilename2, generatorOutput2 } }, AppendAttribute(code));
        }
        protected Task AssertNoGeneration(params string[] code)
        {
            return AssertGeneration(new Dictionary<string, string>(), AppendAttribute(code));
        }
        protected Task AssertMultipleGenerations(IDictionary<string, string> primaryGeneratorOutputs, params string[] code)
        {
            return AssertGeneration(primaryGeneratorOutputs, AppendAttribute(code));
        }

        private string[] AppendAttribute(string[] code)
        {
            return code.Union(new List<string> { ExpectedAttributeContents }).ToArray();
        }
    }
}
