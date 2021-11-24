using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Constructors.Generators.Definitions
{
    public class ParentNamespace
    {
        public string NamespaceName { get; set; }
        public List<string> UsingDirectives { get; set; } = new List<string>();

        public ParentNamespace(NamespaceDeclarationSyntax syntax)
        {
            NamespaceName = syntax.Name.ToString();
            UsingDirectives = syntax.ChildNodes().OfType<UsingDirectiveSyntax>().Select(p => p.ToFullString()).ToList();
        }
    }
}