using Microsoft.CodeAnalysis;

namespace SlowFox.UnitTestMocks.MSTest.Extensions
{
    internal static class ITypeSymbolExtensions
    {
        internal static bool CanBeMocked(this ITypeSymbol symbol)
        {
            if (!symbol.IsReferenceType)
            {
                return false;
            }

            if (symbol.TypeKind == TypeKind.Interface)
            {
                return true;
            }

            if (symbol.TypeKind == TypeKind.Delegate)
            {
                return true;
            }

            return !symbol.IsSealed && !symbol.IsStatic;
        }
    }
}
