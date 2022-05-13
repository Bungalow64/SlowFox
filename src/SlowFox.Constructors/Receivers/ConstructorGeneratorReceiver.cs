using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Definitions;
using SlowFox.Core.Logic;

namespace SlowFox.Constructors.Receivers
{
    internal class ConstructorGeneratorReceiver : ISyntaxContextReceiver
    {
        public TargetClasses ClassesToAugment { get; private set; } = new TargetClasses();

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