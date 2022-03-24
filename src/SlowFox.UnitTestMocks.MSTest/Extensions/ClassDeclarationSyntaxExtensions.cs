using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    }
}
