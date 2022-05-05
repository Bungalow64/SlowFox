using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace SlowFox.Constructors.Tests.Base
{
    public abstract class BaseMultiTest<TGenerator1, TGenerator2>
        where TGenerator1 : ISourceGenerator, new()
        where TGenerator2 : ISourceGenerator, new()
    {
        protected const string GenericCode = @"public class Program
    {
        public static void Main(string[] args)
        {
        }
    }";

        protected async Task AssertMultipleGenerations(IDictionary<string, string> primaryGeneratorOutputs, IDictionary<string, string> secondaryGeneratorOutputs, params string[] code)
        {
            var tester = new Verifiers.CSharpMultipleSourceGeneratorVerifier<TGenerator1, TGenerator2>.Test();
            foreach (string codeItem in code)
            {
                tester.TestState.Sources.Add(codeItem);
            }
            foreach (var output in primaryGeneratorOutputs)
            {
                tester.TestState.GeneratedSources.Add((typeof(TGenerator1), output.Key, SourceText.From(output.Value, Encoding.UTF8, SourceHashAlgorithm.Sha1)));
            }
            foreach (var output in secondaryGeneratorOutputs)
            {
                tester.TestState.GeneratedSources.Add((typeof(TGenerator2), output.Key, SourceText.From(output.Value, Encoding.UTF8, SourceHashAlgorithm.Sha1)));
            }

            await tester.RunAsync();
        }

        protected (GeneratorDriver driver, Compilation output, ImmutableArray<Diagnostic> diagnostics) RunGenerator(string code)
        {
            Compilation inputCompilation = CreateCompilation(code);

            var generator1 = new TGenerator1();
            var generator2 = new TGenerator2();

            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator1, generator2);

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
