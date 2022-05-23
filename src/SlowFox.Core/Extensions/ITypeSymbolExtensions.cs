using Microsoft.CodeAnalysis;

namespace SlowFox.Core.Extensions
{
    /// <summary>
    /// The extension methods for <see cref="ITypeSymbol"/> objects
    /// </summary>
    public static class ITypeSymbolExtensions
    {
        /// <summary>
        /// Whether the specified type can be mocked
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
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
