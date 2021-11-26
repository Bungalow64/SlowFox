using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace SlowFox.Constructors.Tests.Base
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

        protected Task AssertGeneration(string expectedOutput, string expectedFilename)
        {
            return AssertGeneration(expectedOutput, expectedFilename, GenericCode);
        }

        protected async Task AssertGeneration(string expectedOutput, string expectedFilename, string code)
        {
            await new Verifiers.CSharpSourceGeneratorVerifier<TGenerator>.Test
            {
                TestState =
                {
                    Sources = { code },
                    GeneratedSources =
                    {
                        (typeof(TGenerator), expectedFilename, SourceText.From(expectedOutput, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
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
