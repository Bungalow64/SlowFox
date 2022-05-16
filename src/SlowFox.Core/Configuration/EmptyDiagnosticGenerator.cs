using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;

namespace SlowFox.Core.Configuration
{
    public class EmptyDiagnosticGenerator : IDiagnosticGenerator
    {
        public virtual DiagnosticDescriptor UnexpectedErrorDiagnostic => null;
        public virtual bool HasUnexpectedErrorDiagnostic => false;

        public virtual DiagnosticDescriptor InvalidConfigOptionDiagnostic => null;

        public virtual bool HasInvalidConfigOptionDiagnostic => false;

        public virtual DiagnosticDescriptor NoTypeDiagnostic => null;

        public virtual bool HasNoTypeDiagnostic => false;

        public virtual DiagnosticDescriptor MissingDependencyDiagnostic => null;

        public virtual bool HasMissingDependencyDiagnostic => false;
    }
}
