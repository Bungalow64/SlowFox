using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Core.Definitions
{
    /// <summary>
    /// The definition of a parent namespace
    /// </summary>
    public class ParentNamespace
    {
        /// <summary>
        /// The name of the namespace
        /// </summary>
        public string NamespaceName { get; set; }
        /// <summary>
        /// The collection of using directives for the namespace
        /// </summary>
        public IEnumerable<string> UsingDirectives { get; set; } = new List<string>();

        /// <summary>
        /// Constructs a new <see cref="ParentNamespace"/> instance
        /// </summary>
        /// <param name="syntax"></param>
        public ParentNamespace(BaseNamespaceDeclarationSyntax syntax)
        {
            if (syntax is null)
            {
                return;
            }
            NamespaceName = syntax.Name.ToString();
            UsingDirectives = syntax.ChildNodes().OfType<UsingDirectiveSyntax>().Select(p => p.ToFullString());
        }
    }
}