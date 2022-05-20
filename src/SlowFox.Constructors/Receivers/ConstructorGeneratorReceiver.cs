using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Definitions;
using SlowFox.Core.GeneratorLogic.Constructor.Logic;

namespace SlowFox.Constructors.Receivers
{
    /// <summary>
    /// The receiver for generating constructors
    /// </summary>
    internal class ConstructorGeneratorReceiver : ISyntaxContextReceiver
    {
        /// <summary>
        /// The list of classes that have been detected as needing generation
        /// </summary>
        public TargetClasses ClassesToAugment { get; private set; } = new TargetClasses();

        /// <inheritdoc/>
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            (ClassDeclarationSyntax cds, AttributeSyntax attribute, ITypeSymbol baseType) = DependencyReader.FindAttribute(context.SemanticModel, context.Node);

            if (attribute != null)
            {
                ClassesToAugment.Add(cds, attribute, baseType);
            }
        }
    }
}