﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.UnitTestMocks.MSTest.Receivers
{
    internal class MockGeneratorReceiver : ISyntaxContextReceiver
    {
        private const string InjectableClassAttributeName1 = "SlowFox.InjectMocksAttribute";
        private const string InjectableClassAttributeName2 = "SlowFox.InjectMocks";
        private const string InjectableClassAttributeName3 = "InjectMocks";

        public List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>> ClassesToAugment { get; private set; } = new List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax cds)
            {
                bool matches(string type)
                {
                    if (string.IsNullOrEmpty(type))
                    {
                        return false;
                    }
                    return type.Equals(InjectableClassAttributeName1) || type.Equals(InjectableClassAttributeName2) || type.Equals(InjectableClassAttributeName3);
                }

                var attribute = cds
                    .DescendantNodes()
                    .OfType<AttributeSyntax>()
                    .Where(p => p.DescendantTokens().Any(dt => dt.IsKind(SyntaxKind.IdentifierToken) && dt.Parent != null && matches(context.SemanticModel.GetTypeInfo(dt.Parent).Type?.ToString())))
                    .FirstOrDefault();

                if (attribute != null)
                {
                    ClassesToAugment.Add(new KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>(cds, attribute));
                }
            }
        }
    }
}