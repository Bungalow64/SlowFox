using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Tests.Shared.Verifiers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SlowFox.Tests.Shared.Base
{
    public abstract class BaseTest<TGenerator>
        where TGenerator : ISourceGenerator, new()
    {
        protected const string GenericCode = @"public class Program
    {
        public static void Main(string[] args)
        {
        }
    }";

        protected abstract IList<string> MetadataReferences { get; }

        protected Task AssertGeneration(string expectedOutput, string expectedFilename)
        {
            return AssertGeneration(new Dictionary<string, string> { { expectedOutput, expectedFilename } }, GenericCode);
        }

        protected Task AssertGeneration(IDictionary<string, string> primaryGeneratorOutputs, params string[] code)
        {
            return AssertGeneration(primaryGeneratorOutputs, null, Array.Empty<DiagnosticResult>(), code);
        }

        protected Task AssertGenerationWithConfig(IDictionary<string, string> primaryGeneratorOutputs, string config, params string[] code)
        {
            return AssertGeneration(primaryGeneratorOutputs, config, Array.Empty<DiagnosticResult>(), code);
        }

        protected Task AssertGeneration(IDictionary<string, string> primaryGeneratorOutputs, DiagnosticResult[] expectedDiagnostics, params string[] code)
        {
            return AssertGeneration(primaryGeneratorOutputs, null, expectedDiagnostics, code);
        }

        protected async Task AssertGeneration(IDictionary<string, string> primaryGeneratorOutputs, string config, DiagnosticResult[] expectedDiagnostics, params string[] code)
        {
            var tester = new CSharpSourceGeneratorVerifier<TGenerator>.Test();
            foreach (string codeItem in code)
            {
                tester.TestState.Sources.Add(codeItem);
            }
            foreach (var output in primaryGeneratorOutputs)
            {
                tester.TestState.GeneratedSources.Add((typeof(TGenerator), output.Key, SourceText.From(output.Value, Encoding.UTF8, SourceHashAlgorithm.Sha1)));
            }
            foreach (var reference in MetadataReferences)
            {
                tester.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(reference));
            }
            foreach (var diagnostic in expectedDiagnostics)
            {
                tester.TestState.ExpectedDiagnostics.Add(diagnostic);
            }

            if (config is not null)
            {
                tester.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", config));
            }

            await tester.RunAsync();
        }

        protected (GeneratorDriver driver, Compilation output, ImmutableArray<Diagnostic> diagnostics) RunGenerator(string code)
        {
            Compilation inputCompilation = CreateCompilation(code);

            var generator = new TGenerator();

            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            return (driver, outputCompilation, diagnostics);
        }

        private static Compilation CreateCompilation(string source)
           => CSharpCompilation.Create("compilation",
               new[] { CSharpSyntaxTree.ParseText(source) },
               new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
               new CSharpCompilationOptions(OutputKind.ConsoleApplication));
    }
}
