using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.UnitTestMocks.Generators;
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

        protected override string DependencyFilename => _dependencyFilename;
        protected override string CustomUsing => _customUsing;
        protected override string RootConfig => _rootConfig;
        protected override string InitAttribute => _initAttribute;
        protected override string InitMethodName => _initMethodName;
        protected override IDiagnosticGenerator Diagnostics => diagnostics;
    }
}
