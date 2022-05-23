using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration;
using SlowFox.Core.Configuration.Abstract;
using System.Collections.Generic;

namespace SlowFox.Core.GeneratorLogic.UnitTestMocks.Configuration
{
    /// <summary>
    /// The configuration for the unit test mock generator
    /// </summary>
    public class CustomConfiguration
    {
        /// <summary>
        /// Whether underscores should be ignored
        /// </summary>
        public bool SkipUnderscore { get; set; }
        /// <summary>
        /// Whether the mock objects should be set as loose
        /// </summary>
        public bool UseLoose { get; set; }

        /// <summary>
        /// Instantiates the configuration
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rootConfig"></param>
        /// <param name="targetClass"></param>
        /// <param name="diagnostic"></param>
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
