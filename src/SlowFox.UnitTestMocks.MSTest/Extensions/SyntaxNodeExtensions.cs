using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Definitions;
using System.Collections.Generic;

namespace SlowFox.UnitTestMocks.MSTest.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        internal static List<(string className, string modifiers)> GetParentClasses(this SyntaxNode parent)
        {
            var names = new List<(string className, string modifiers)>();

            while (parent != null && !(parent is NamespaceDeclarationSyntax))
            {
                if (parent is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    names.Add((classDeclarationSyntax.Identifier.Text, classDeclarationSyntax.GetModifiers()));
                }
                parent = parent.Parent;
            }

            return names;
        }

        internal static List<ParentNamespace> GetNamespace(this SyntaxNode parent)
        {
            List<ParentNamespace> namespaces = new List<ParentNamespace>();

            while (parent != null)
            {
                if (parent is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                {
                    namespaces.Add(new ParentNamespace(namespaceDeclaration));
                }
                parent = parent.Parent;
            }

            namespaces.Reverse();
            return namespaces;
        }
    }
}
