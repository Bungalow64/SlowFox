using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Definitions;
using System.Collections.Generic;

namespace SlowFox.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SyntaxNode"/> objects
    /// </summary>
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Gets the list of parent classes
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<(string className, string modifiers)> GetParentClasses(this SyntaxNode parent)
        {
            var names = new List<(string className, string modifiers)>();

            while (!(parent is null) && !(parent is NamespaceDeclarationSyntax))
            {
                if (parent is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    names.Add((classDeclarationSyntax.Identifier.Text, classDeclarationSyntax.GetModifiers()));
                }
                parent = parent.Parent;
            }

            return names;
        }

        /// <summary>
        /// Gets the list of namespaces in which the object is defined
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<ParentNamespace> GetNamespace(this SyntaxNode parent)
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
