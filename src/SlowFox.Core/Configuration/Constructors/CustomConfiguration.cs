using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration.Abstract;
using System;

namespace SlowFox.Core.Configuration.Constructors
{
    public class CustomConfiguration
    {
        public bool SkipUnderscore { get; set; }
        public bool IncludeNullCheck { get; set; }

        public CustomConfiguration(GeneratorExecutionContext context, string rootConfig, ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attribute, IDiagnosticGenerator diagnostic)
        {
            var options = context.AnalyzerConfigOptions.GetOptions(classDeclarationSyntax.SyntaxTree);
            if (options != null)
            {
                SkipUnderscore = OptionReader.Get(context, options, rootConfig, "skip_underscores", () => attribute.GetLocation(), diagnostic);
                IncludeNullCheck = OptionReader.Get(context, options, rootConfig, "include_nullcheck", () => attribute.GetLocation(), diagnostic);
            }
        }
    }
}
