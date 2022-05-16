﻿using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration;

namespace SlowFox.Constructors.Diagnostics
{
    public class DiagnosticGenerator : EmptyDiagnosticGenerator
    {
        private static readonly DiagnosticDescriptor _unexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFCT001",
            title: "Couldn't generate constructor",
            messageFormat: "Couldn't generate the constructor for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.Constructors/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor _invalidConfigOptionDiagnostic = new DiagnosticDescriptor(
            id: "SFCT002",
            title: "The config option has an unexpected value",
            messageFormat: "Config '{0}' has an unexpected value: {1}.  Allowed options are: {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.Constructors/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        public override DiagnosticDescriptor UnexpectedErrorDiagnostic => _unexpectedErrorDiagnostic;

        public override DiagnosticDescriptor InvalidConfigOptionDiagnostic => _invalidConfigOptionDiagnostic;

        public override bool HasUnexpectedErrorDiagnostic => true;

        public override bool HasInvalidConfigOptionDiagnostic => true;
    }
}
