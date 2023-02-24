using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;

namespace SlowFox.Core.Configuration
{
    /// <summary>
    /// Class defining a diagnostic generator that won't return any diagnostic warnings.  This is to be used for cases where errors should be ignored.
    /// </summary>
    public class EmptyDiagnosticGenerator : IDiagnosticGenerator
    {
        /// <inheritdoc/>
        public virtual DiagnosticDescriptor UnexpectedErrorDiagnostic => null;
        /// <inheritdoc/>
        public virtual bool HasUnexpectedErrorDiagnostic => false;
        /// <inheritdoc/>
        public virtual DiagnosticDescriptor InvalidConfigOptionDiagnostic => null;
        /// <inheritdoc/>
        public virtual bool HasInvalidConfigOptionDiagnostic => false;
        /// <inheritdoc/>
        public virtual DiagnosticDescriptor NoTypeDiagnostic => null;
        /// <inheritdoc/>
        public virtual bool HasNoTypeDiagnostic => false;
        /// <inheritdoc/>
        public virtual DiagnosticDescriptor MissingDependencyDiagnostic => null;
        /// <inheritdoc/>
        public virtual bool HasMissingDependencyDiagnostic => false;
        /// <inheritdoc/>
        public virtual DiagnosticDescriptor AbstractTargetDiagnostic => null;
        /// <inheritdoc/>
        public virtual bool HasAbstractTargetDiagnostic => false;
    }
}
