using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

namespace SlowFox.UnitTestMocks.MSTest.Tests.Base
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

        protected Task AssertGeneration(string generator1output, string generator2output, string generator1filename, string generator2filename)
        {
            return AssertGeneration(generator1output, generator2output, generator1filename, generator2filename, GenericCode);
        }
        protected Task AssertNoSecondaryGeneration(string generator1output, string generator2output)
        {
            return AssertNoSecondaryGeneration(generator1output, generator2output, GenericCode);
        }

        protected async Task AssertGeneration(string generator1output, string generator2output, string generator1filename, string generator2filename, params string[] code)
        {
            if (code.Length == 1)
            {
                await new Verifiers.CSharpMultipleSourceGeneratorVerifier<TGenerator1, TGenerator2>.Test
                {
                    TestState =
                    {
                        Sources = { code[0] },
                        GeneratedSources =
                        {
                            (typeof(TGenerator1), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                            (typeof(TGenerator2), generator2filename, SourceText.From(generator2output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                        },
                        AdditionalReferences =
                        {
                            MetadataReference.CreateFromFile("Moq.dll"),
                            MetadataReference.CreateFromFile("Microsoft.VisualStudio.TestPlatform.TestFramework.dll")
                        }
                    }
                }.RunAsync();
            }
            else
            {
                await new Verifiers.CSharpMultipleSourceGeneratorVerifier<TGenerator1, TGenerator2>.Test
                {
                    TestState =
                    {
                        Sources = { code[0], code[1] },
                        GeneratedSources =
                        {
                            (typeof(TGenerator1), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                            (typeof(TGenerator2), generator2filename, SourceText.From(generator2output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                        },
                        AdditionalReferences =
                        {
                            MetadataReference.CreateFromFile("Moq.dll"),
                            MetadataReference.CreateFromFile("Microsoft.VisualStudio.TestPlatform.TestFramework.dll")
                        }
                    }
                }.RunAsync();
            }
        }

        protected async Task AssertTwoGeneration(string generator1output, string generator2output, string generator3output, string generator1filename, string generator2filename, string generator3filename, params string[] code)
        {
            if (code.Length == 1)
            {
                await new Verifiers.CSharpMultipleSourceGeneratorVerifier<TGenerator1, TGenerator2>.Test
                {
                    TestState =
                {
                    Sources = { code[0] },
                    GeneratedSources =
                    {
                        (typeof(TGenerator1), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(TGenerator1), generator2filename, SourceText.From(generator2output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(TGenerator2), generator3filename, SourceText.From(generator3output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                    AdditionalReferences =
                    {
                        MetadataReference.CreateFromFile("Moq.dll")
                    }
                }
                }.RunAsync();
            }
            else
            {
                await new Verifiers.CSharpMultipleSourceGeneratorVerifier<TGenerator1, TGenerator2>.Test
                {
                    TestState =
                {
                    Sources = { code[0], code[1] },
                    GeneratedSources =
                    {
                        (typeof(TGenerator1), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(TGenerator1), generator2filename, SourceText.From(generator2output, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(TGenerator2), generator3filename, SourceText.From(generator3output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                    AdditionalReferences =
                    {
                        MetadataReference.CreateFromFile("Moq.dll")
                    }
                }
                }.RunAsync();
            }
        }

        protected async Task AssertNoSecondaryGeneration(string generator1output, string generator1filename, params string[] code)
        {
            if (code.Length == 1)
            {
                await new Verifiers.CSharpSourceGeneratorVerifier<TGenerator2>.Test
                {
                    TestState =
                {
                    Sources = { code[0] },
                    GeneratedSources =
                    {
                        (typeof(TGenerator2), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                    AdditionalReferences =
                    {
                        MetadataReference.CreateFromFile("Moq.dll")
                    }
                }
                }.RunAsync();
            }
            else
            {
                await new Verifiers.CSharpSourceGeneratorVerifier<TGenerator2>.Test
                {
                    TestState =
                {
                    Sources = { code[0], code[1] },
                    GeneratedSources =
                    {
                        (typeof(TGenerator2), generator1filename, SourceText.From(generator1output, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                    AdditionalReferences =
                    {
                        MetadataReference.CreateFromFile("Moq.dll")
                    }
                }
                }.RunAsync();
            }
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
