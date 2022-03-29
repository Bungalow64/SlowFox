using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.UnitTestMocks.MSTest.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.UnitTestMocks.MSTest.Receivers
{
    internal class MockGeneratorReceiver : ISyntaxContextReceiver
    {
        public List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>> ClassesToAugment { get; private set; } = new List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
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
    }
}