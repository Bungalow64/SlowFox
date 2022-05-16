using Microsoft.CodeAnalysis;

namespace SlowFox.Core.Configuration.Abstract
{
    public interface IDiagnosticGenerator
    {
        DiagnosticDescriptor UnexpectedErrorDiagnostic { get; }
        bool HasUnexpectedErrorDiagnostic { get; }
        DiagnosticDescriptor InvalidConfigOptionDiagnostic { get; }
        bool HasInvalidConfigOptionDiagnostic { get; }
        DiagnosticDescriptor NoTypeDiagnostic { get; }
        bool HasNoTypeDiagnostic { get; }
        DiagnosticDescriptor MissingDependencyDiagnostic { get; }
        bool HasMissingDependencyDiagnostic { get; }
    }
}
