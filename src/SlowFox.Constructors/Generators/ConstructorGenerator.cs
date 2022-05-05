using Microsoft.CodeAnalysis;
using SlowFox.Constructors.Receivers;

namespace SlowFox.Constructors.Generators
{
    /// <summary>
    /// Source generator for generating constructors
    /// </summary>
    [Generator]
    public sealed partial class ConstructorGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor UnexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFCT001",
            title: "Couldn't generate constructor",
            messageFormat: "Couldn't generate the constructor for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.Constructors/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ConstructorGeneratorReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is ConstructorGeneratorReceiver syntaxReceiver))
            {
                return;
            }

            syntaxReceiver.ClassesToAugment.Process(UnexpectedErrorDiagnostic, context);
        }
    }
}