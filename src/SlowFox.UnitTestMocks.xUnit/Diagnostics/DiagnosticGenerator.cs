using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration;

namespace SlowFox.UnitTestMocks.xUnit.Diagnostics
{
    /// <summary>
    /// The diagnostics for the generator
    /// </summary>
    public class DiagnosticGenerator : EmptyDiagnosticGenerator
    {
        private static readonly DiagnosticDescriptor _unexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFMKX001",
            title: "Couldn't generate mocks",
            messageFormat: "Couldn't generate the mocks for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/blob/main/src/SlowFox.UnitTestMocks.xUnit/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _noTypeDiagnostic = new DiagnosticDescriptor(
            id: "SFMKX002",
            title: "Only classes can be the target of the InjectMocksAttribute",
            messageFormat: "Incorrect type found for object '{0}'.  Expected IdentifierNameSyntax, but found {1}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/blob/main/src/SlowFox.UnitTestMocks.xUnit/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _missingDependencyDiagnostic = new DiagnosticDescriptor(
            id: "SFMKX003",
            title: "A required dependency has not been found",
            messageFormat: "{1} is required in {0}, but it has not been found",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/blob/main/src/SlowFox.UnitTestMocks.xUnit/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _invalidConfigOptionDiagnostic = new DiagnosticDescriptor(
            id: "SFMKX004",
            title: "The config option has an unexpected value",
            messageFormat: "Config '{0}' has an unexpected value: {1}.  Allowed options are: {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/blob/main/src/SlowFox.UnitTestMocks.xUnit/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _abstractTargetDiagnostic = new DiagnosticDescriptor(
            id: "SFMKX005",
            title: "Abstract classes cannot be the target of the InjectMocksAttribute",
            messageFormat: "{0} is an abstract class",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/blob/main/src/SlowFox.UnitTestMocks.xUnit/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        /// <inheritdoc/>
        public override DiagnosticDescriptor UnexpectedErrorDiagnostic => _unexpectedErrorDiagnostic;

        /// <inheritdoc/>
        public override DiagnosticDescriptor NoTypeDiagnostic => _noTypeDiagnostic;

        /// <inheritdoc/>
        public override DiagnosticDescriptor MissingDependencyDiagnostic => _missingDependencyDiagnostic;

        /// <inheritdoc/>
        public override DiagnosticDescriptor InvalidConfigOptionDiagnostic => _invalidConfigOptionDiagnostic;

        /// <inheritdoc/>
        public override DiagnosticDescriptor AbstractTargetDiagnostic => _abstractTargetDiagnostic;

        /// <inheritdoc/>
        public override bool HasUnexpectedErrorDiagnostic => true;

        /// <inheritdoc/>
        public override bool HasInvalidConfigOptionDiagnostic => true;

        /// <inheritdoc/>
        public override bool HasNoTypeDiagnostic => true;

        /// <inheritdoc/>
        public override bool HasMissingDependencyDiagnostic => true;

        /// <inheritdoc/>
        public override bool HasAbstractTargetDiagnostic => true;
    }
}
