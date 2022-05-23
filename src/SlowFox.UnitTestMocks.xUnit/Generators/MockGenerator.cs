using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Generators;
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

        /// <inheritdoc/>
        protected override string DependencyFilename => _dependencyFilename;
        /// <inheritdoc/>
        protected override string CustomUsing => _customUsing;
        /// <inheritdoc/>
        protected override string RootConfig => _rootConfig;
        /// <inheritdoc/>
        protected override string InitAttribute => null;
        /// <inheritdoc/>
        protected override string InitMethodName => null;
        /// <inheritdoc/>
        protected override IDiagnosticGenerator Diagnostics => diagnostics;
    }
}
