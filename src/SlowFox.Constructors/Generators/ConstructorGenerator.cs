using Microsoft.CodeAnalysis;
using SlowFox.Constructors.Diagnostics;
using SlowFox.Constructors.Receivers;
using SlowFox.Core.Configuration.Abstract;

namespace SlowFox.Constructors.Generators
{
    /// <summary>
    /// Source generator for generating constructors
    /// </summary>
    [Generator]
    public sealed partial class ConstructorGenerator : ISourceGenerator
    {
        private readonly IDiagnosticGenerator diagnostics = new DiagnosticGenerator();

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

            syntaxReceiver.ClassesToAugment.Process(diagnostics, context);
        }
    }
}