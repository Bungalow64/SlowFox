using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Definitions;
using SlowFox.Core.Extensions;
using SlowFox.Core.GeneratorLogic.Constructor.Logic;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Core.GeneratorLogic.UnitTestMocks.Receivers
{
    /// <summary>
    /// The receiver for generating unit test mocks
    /// </summary>
    public class MockGeneratorReceiver : ISyntaxContextReceiver
    {
        /// <summary>
        /// The list of classes that have been detected as needing generation
        /// </summary>
        public List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>> ClassesToAugment { get; private set; } = new List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>>();

        /// <summary>
        /// The list of classes that are to have constructors generated
        /// </summary>
        public TargetClasses ClassesToInject { get; private set; } = new TargetClasses();

        /// <inheritdoc/>
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            FindMockClasses(context);
            FindInjectClasses(context);
        }

        private void FindMockClasses(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax cds)
            {
                var attribute = cds
                    .FindAttributes(context.SemanticModel, "SlowFox", "InjectMocks")
                    .FirstOrDefault();

                if (attribute != null)
                {
                    ClassesToAugment.Add(new KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>(cds, attribute));
                }
            }
        }

        private void FindInjectClasses(GeneratorSyntaxContext context)
        {
            (ClassDeclarationSyntax cds, AttributeSyntax attribute, ITypeSymbol baseType) = DependencyReader.FindAttribute(context.SemanticModel, context.Node);

            if (attribute != null)
            {
                ClassesToInject.Add(cds, attribute, baseType);
            }
        }
    }
}