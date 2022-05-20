using Microsoft.CodeAnalysis;

namespace SlowFox.Core.Configuration.Abstract
{
    /// <summary>
    /// The interface for diagnostic definitions
    /// </summary>
    public interface IDiagnosticGenerator
    {
        /// <summary>
        /// The diagnostic for unexpected errors
        /// </summary>
        DiagnosticDescriptor UnexpectedErrorDiagnostic { get; }
        /// <summary>
        /// Whether there is a diagnostic for unexpected errors
        /// </summary>
        bool HasUnexpectedErrorDiagnostic { get; }
        /// <summary>
        /// The diagnostic for an invalid config option
        /// </summary>
        DiagnosticDescriptor InvalidConfigOptionDiagnostic { get; }
        /// <summary>
        /// Whether there is a diagnostic for an invalid config option
        /// </summary>
        bool HasInvalidConfigOptionDiagnostic { get; }
        /// <summary>
        /// The diagnostic for when a type is missing
        /// </summary>
        DiagnosticDescriptor NoTypeDiagnostic { get; }
        /// <summary>
        /// Whether there is a diagnostic for when a type is missing
        /// </summary>
        bool HasNoTypeDiagnostic { get; }
        /// <summary>
        /// The diagnostic for when a dependency is missing
        /// </summary>
        DiagnosticDescriptor MissingDependencyDiagnostic { get; }
        /// <summary>
        /// Whether there is a diagnostic for when a dependency is missing
        /// </summary>
        bool HasMissingDependencyDiagnostic { get; }
    }
}
