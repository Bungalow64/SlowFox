using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.UnitTestMocks.MSTest.Logic;
using System.Collections.Generic;

namespace SlowFox.UnitTestMocks.MSTest.Configuration
{
    internal class CustomConfiguration
    {
        public bool SkipUnderscore { get; set; }
        public bool UseLoose { get; set; }

        internal CustomConfiguration(GeneratorExecutionContext context, KeyValuePair<ClassDeclarationSyntax, AttributeSyntax> targetClass)
        {
            var options = context.AnalyzerConfigOptions.GetOptions(targetClass.Key.SyntaxTree);
            if (options != null)
            {
                SkipUnderscore = OptionReader.Get(context, options, "skip_underscores", () => targetClass.Value.GetLocation());
                UseLoose = OptionReader.Get(context, options, "use_loose", () => targetClass.Value.GetLocation());
            }
        }
    }
}
