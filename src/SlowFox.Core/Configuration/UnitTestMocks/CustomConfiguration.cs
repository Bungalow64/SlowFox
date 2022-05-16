using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration.Abstract;
using System;
using System.Collections.Generic;

namespace SlowFox.Core.Configuration.UnitTestMocks
{
    public class CustomConfiguration
    {
        public bool SkipUnderscore { get; set; }
        public bool UseLoose { get; set; }

        public CustomConfiguration(GeneratorExecutionContext context, string rootConfig, KeyValuePair<ClassDeclarationSyntax, AttributeSyntax> targetClass, IDiagnosticGenerator diagnostic)
        {
            var options = context.AnalyzerConfigOptions.GetOptions(targetClass.Key.SyntaxTree);
            if (options != null)
            {
                SkipUnderscore = OptionReader.Get(context, options, rootConfig, "skip_underscores", () => targetClass.Value.GetLocation(), diagnostic);
                UseLoose = OptionReader.Get(context, options, rootConfig, "use_loose", () => targetClass.Value.GetLocation(), diagnostic);
            }
        }
    }
}
