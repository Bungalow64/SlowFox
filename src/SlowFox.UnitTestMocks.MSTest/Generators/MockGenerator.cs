using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Generators;
using SlowFox.UnitTestMocks.MSTest.Diagnostics;

namespace SlowFox.UnitTestMocks.MSTest.Generators
{
    /// <summary>
    /// Source generator for generating mocks
    /// </summary>
    [Generator]
    public sealed partial class MockGenerator : BaseMockGenerator
    {
        private const string _dependencyFilename = "Microsoft.VisualStudio.TestPlatform.TestFramework";
        private const string _customUsing = "using Microsoft.VisualStudio.TestTools.UnitTesting;";
        private const string _rootConfig = "slowfox_generation.unit_test_mocks.mstest.";
        private const string _initAttribute = "TestInitialize";
        private const string _initMethodName = "Init";
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
