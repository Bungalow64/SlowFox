using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.UnitTestMocks.Generators;
using SlowFox.UnitTestMocks.xUnit.Diagnostics;

namespace SlowFox.UnitTestMocks.xUnit.Generators
{
    /// <summary>
    /// Source generator for generating mocks
    /// </summary>
    [Generator]
    public sealed partial class MockGenerator : BaseMockGenerator
    {
        private const string _dependencyFilename = "xunit.core";
        private const string _customUsing = "using Xunit;";
        private const string _rootConfig = "slowfox_generation.unit_test_mocks.xunit.";
        private readonly IDiagnosticGenerator diagnostics = new DiagnosticGenerator();

        protected override string DependencyFilename => _dependencyFilename;
        protected override string CustomUsing => _customUsing;
        protected override string RootConfig => _rootConfig;
        protected override string InitAttribute => null;
        protected override string InitMethodName => null;
        protected override IDiagnosticGenerator Diagnostics => diagnostics;
    }
}
