using Microsoft.CodeAnalysis;

namespace SlowFox.Core.Extensions
{
    public static class ITypeSymbolExtensions
    {
        public static bool CanBeMocked(this ITypeSymbol symbol)
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
