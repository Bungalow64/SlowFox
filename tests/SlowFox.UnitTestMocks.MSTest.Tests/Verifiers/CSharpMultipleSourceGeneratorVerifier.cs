using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SlowFox.UnitTestMocks.MSTest.Tests.Verifiers
{
    public static class CSharpMultipleSourceGeneratorVerifier<TSourceGenerator1, TSourceGenerator2>
        where TSourceGenerator1 : ISourceGenerator, new()
        where TSourceGenerator2 : ISourceGenerator, new()
    {
        public class Test : CSharpSourceGeneratorTest<TSourceGenerator1, XUnitVerifier>
        {
            public Test()
            {
            }

            protected override CompilationOptions CreateCompilationOptions()
            {
                var compilationOptions = base.CreateCompilationOptions();
                return compilationOptions.WithSpecificDiagnosticOptions(
                     compilationOptions.SpecificDiagnosticOptions.SetItems(GetNullableWarningsFromCompiler()));
            }

            public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.Default;

            private static ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
            {
                string[] args = { "/warnaserror:nullable" };
                var commandLineArguments = CSharpCommandLineParser.Default.Parse(args, baseDirectory: Environment.CurrentDirectory, sdkDirectory: Environment.CurrentDirectory);
                var nullableWarnings = commandLineArguments.CompilationOptions.SpecificDiagnosticOptions;

                return nullableWarnings;
            }

            protected override ParseOptions CreateParseOptions()
            {
                return ((CSharpParseOptions)base.CreateParseOptions()).WithLanguageVersion(LanguageVersion);
            }

            protected override IEnumerable<ISourceGenerator> GetSourceGenerators()
            {
                return base.GetSourceGenerators().ToArray().Union(new List<ISourceGenerator>{ new TSourceGenerator2() });
            }
        }
    }
}