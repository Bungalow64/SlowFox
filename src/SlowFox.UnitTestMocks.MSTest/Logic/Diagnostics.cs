using Microsoft.CodeAnalysis;

namespace SlowFox.UnitTestMocks.MSTest.Logic
{
    internal static class Diagnostics
    {
        internal static readonly DiagnosticDescriptor UnexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFMK001",
            title: "Couldn't generate mocks",
            messageFormat: "Couldn't generate the mocks for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        internal static readonly DiagnosticDescriptor NoTypeDiagnostic = new DiagnosticDescriptor(
            id: "SFMK002",
            title: "Only classes can be the target of the InjectMocksAttribute",
            messageFormat: "Incorrect type found for object '{0}'.  Expected IdentifierNameSyntax, but found {1}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        internal static readonly DiagnosticDescriptor MissingDependencyDiagnostic = new DiagnosticDescriptor(
            id: "SFMK003",
            title: "A required dependency has not been found",
            messageFormat: "{1} is required in {0}, but it has not been found",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        internal static readonly DiagnosticDescriptor InvalidConfigOptionDiagnostic = new DiagnosticDescriptor(
            id: "SFMK004",
            title: "The config option has an unexpected value",
            messageFormat: "Config '{0}' has an unexpected value: {1}.  Allowed options are: {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);
    }
}
