using Microsoft.CodeAnalysis;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.UnitTestMocks.Generators;
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

        protected override string DependencyFilename => _dependencyFilename;
        protected override string CustomUsing => _customUsing;
        protected override string RootConfig => _rootConfig;
        protected override string InitAttribute => _initAttribute;
        protected override string InitMethodName => _initMethodName;
        protected override IDiagnosticGenerator Diagnostics => diagnostics;
    }
}
