using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Generators;
using SlowFox.UnitTestMocks.NUnit.Diagnostics;

namespace SlowFox.UnitTestMocks.NUnit.Generators
{
    /// <summary>
    /// Source generator for generating mocks
    /// </summary>
    [Generator]
    public sealed partial class MockGenerator : BaseMockGenerator
    {
        private const string _dependencyFilename = "nunit.framework";
        private const string _customUsing = "using NUnit.Framework;";
        private const string _rootConfig = "slowfox_generation.unit_test_mocks.nunit.";
        private const string _initAttribute = "SetUp";
        private const string _initMethodName = "Setup";
        private readonly IDiagnosticGenerator diagnostics = new DiagnosticGenerator();

        /// <inheritdoc/>
        protected override string DependencyFilename => _dependencyFilename;
        /// <inheritdoc/>
        protected override string CustomUsing => _customUsing;
        /// <inheritdoc/>
        protected override string RootConfig => _rootConfig;
        /// <inheritdoc/>
        protected override string InitAttribute => _initAttribute;
        /// <inheritdoc/>
        protected override string InitMethodName => _initMethodName;
        /// <inheritdoc/>
        protected override IDiagnosticGenerator Diagnostics => diagnostics;
    }
}
