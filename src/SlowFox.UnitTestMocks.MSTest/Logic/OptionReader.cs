using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;

namespace SlowFox.UnitTestMocks.MSTest.Logic
{
    internal static class OptionReader
    {
        private const string RootConfig = "slowfox_generation.unit_test_mocks.mstest.";
        private const string AllowedOptions = "true, false";

        internal static bool Get(GeneratorExecutionContext context, AnalyzerConfigOptions options, string key, Func<Location> locationAccess)
        {
            if (options.TryGetValue($"{RootConfig}{key}", out string foundValue))
            {
                if (bool.TryParse(foundValue, out bool foundValueCast))
                {
                    return foundValueCast;
                }
                else
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.InvalidConfigOptionDiagnostic, locationAccess(), $"{RootConfig}{key}", foundValue, AllowedOptions));
                }
            }
            return false;
        }
    }
}
