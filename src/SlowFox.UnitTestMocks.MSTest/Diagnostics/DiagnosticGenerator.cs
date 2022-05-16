using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration;

namespace SlowFox.UnitTestMocks.MSTest.Diagnostics
{
    public class DiagnosticGenerator : EmptyDiagnosticGenerator
    {
        private static readonly DiagnosticDescriptor _unexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFMKM001",
            title: "Couldn't generate mocks",
            messageFormat: "Couldn't generate the mocks for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _noTypeDiagnostic = new DiagnosticDescriptor(
            id: "SFMKM002",
            title: "Only classes can be the target of the InjectMocksAttribute",
            messageFormat: "Incorrect type found for object '{0}'.  Expected IdentifierNameSyntax, but found {1}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _missingDependencyDiagnostic = new DiagnosticDescriptor(
            id: "SFMKM003",
            title: "A required dependency has not been found",
            messageFormat: "{1} is required in {0}, but it has not been found",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _invalidConfigOptionDiagnostic = new DiagnosticDescriptor(
            id: "SFMKM004",
            title: "The config option has an unexpected value",
            messageFormat: "Config '{0}' has an unexpected value: {1}.  Allowed options are: {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        public override DiagnosticDescriptor UnexpectedErrorDiagnostic => _unexpectedErrorDiagnostic;

        public override DiagnosticDescriptor NoTypeDiagnostic => _noTypeDiagnostic;

        public override DiagnosticDescriptor MissingDependencyDiagnostic => _missingDependencyDiagnostic;

        public override DiagnosticDescriptor InvalidConfigOptionDiagnostic => _invalidConfigOptionDiagnostic;

        public virtual bool HasUnexpectedErrorDiagnostic => true;

        public virtual bool HasInvalidConfigOptionDiagnostic => true;

        public virtual bool HasNoTypeDiagnostic => true;

        public virtual bool HasMissingDependencyDiagnostic => true;
    }
}
