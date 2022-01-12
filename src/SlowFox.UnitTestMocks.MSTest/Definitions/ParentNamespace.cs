using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.UnitTestMocks.MSTest.Definitions
{
    internal class ParentNamespace
    {
        public string NamespaceName { get; set; }
        public List<string> UsingDirectives { get; set; } = new List<string>();

        public ParentNamespace(BaseNamespaceDeclarationSyntax syntax)
        {
            if (syntax is null)
            {
                return;
            }
            NamespaceName = syntax.Name.ToString();
            UsingDirectives = syntax.ChildNodes().OfType<UsingDirectiveSyntax>().Select(p => p.ToFullString()).ToList();
        }
    }
}