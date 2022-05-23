using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using SlowFox.Core.Configuration.Abstract;
using System;

namespace SlowFox.Core.Configuration
{
    internal static class OptionReader
    {
        private const string AllowedOptions = "true, false";

        internal static bool Get(GeneratorExecutionContext context, AnalyzerConfigOptions options, string rootConfig, string key, Func<Location> locationAccess, IDiagnosticGenerator diagnostics)
        {
            if (options.TryGetValue($"{rootConfig}{key}", out string foundValue))
            {
                if (bool.TryParse(foundValue, out bool foundValueCast))
                {
                    return foundValueCast;
                }
                else
                {
                    if (diagnostics.HasInvalidConfigOptionDiagnostic)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(diagnostics.InvalidConfigOptionDiagnostic, locationAccess(), $"{rootConfig}{key}", foundValue, AllowedOptions));
                    }
                }
            }
            return false;
        }
    }
}
