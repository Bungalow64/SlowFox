using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.UnitTestMocks.MSTest.Extensions
{
    internal static class ClassDeclarationSyntaxExtensions
    {
        internal static string GetModifiers(this ClassDeclarationSyntax classDeclarationSyntax)
        {
            string modifier = string.Empty;
            if (classDeclarationSyntax != null && classDeclarationSyntax.Modifiers != null && classDeclarationSyntax.Modifiers.Any())
            {
                modifier = string.Join(" ", classDeclarationSyntax.Modifiers.Select(p => p.Text));
            }
            if (modifier.IndexOf("partial") < 1)
            {
                modifier += " partial";
            }
            return modifier;
        }

        internal static IEnumerable<AttributeSyntax> FindAttributes(this ClassDeclarationSyntax classDeclarationSyntax, SemanticModel semanticModel, string @namespace, string attributeName)
        {
            if (classDeclarationSyntax is null)
            {
                return Array.Empty<AttributeSyntax>();
            }

            if (attributeName.EndsWith("Attribute"))
            {
                attributeName = attributeName.Substring(0, attributeName.Length - 9);
            }

            string name1 = $"{@namespace}.{attributeName}Attribute";
            string name2 = $"{@namespace}.{attributeName}";
            string name3 = $"{attributeName}";
            
            bool matches(string type)
            {
                if (string.IsNullOrEmpty(type))
                {
                    return false;
                }
                return type.Equals(name1) || type.Equals(name2) || type.Equals(name3);
            }

            return classDeclarationSyntax
                .DescendantNodes()
                .OfType<AttributeSyntax>()
                .Where(p => p.DescendantTokens().Any(dt => dt.IsKind(SyntaxKind.IdentifierToken) && dt.Parent != null && matches(semanticModel.GetTypeInfo(dt.Parent).Type?.ToString())));
        }
    }
}
