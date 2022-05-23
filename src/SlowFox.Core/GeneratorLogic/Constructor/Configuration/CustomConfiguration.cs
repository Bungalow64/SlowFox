using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration;
using SlowFox.Core.Configuration.Abstract;

namespace SlowFox.Core.GeneratorLogic.Constructor.Configuration
{
    /// <summary>
    /// The configuration for the constructor generator
    /// </summary>
    public class CustomConfiguration
    {
        /// <summary>
        /// Whether underscores should be ignored
        /// </summary>
        public bool SkipUnderscore { get; set; }
        /// <summary>
        /// Whether there should be a null check for the constructor parameters
        /// </summary>
        public bool IncludeNullCheck { get; set; }

        /// <summary>
        /// Instantiates the configuration
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rootConfig"></param>
        /// <param name="classDeclarationSyntax"></param>
        /// <param name="attribute"></param>
        /// <param name="diagnostic"></param>
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
