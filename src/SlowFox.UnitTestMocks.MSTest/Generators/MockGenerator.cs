using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.UnitTestMocks.MSTest.Definitions;
using SlowFox.UnitTestMocks.MSTest.Logic;
using SlowFox.UnitTestMocks.MSTest.Receivers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SlowFox.UnitTestMocks.MSTest.Generators
{
    /// <summary>
    /// Source generator for generating mocks
    /// </summary>
    [Generator]
    public sealed partial class MockGenerator : ISourceGenerator
    {
        private const string RootConfig = "slowfox_generation.unit_test_mocks.mstest.";
        private static readonly DiagnosticDescriptor UnexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFMK001",
            title: "Couldn't generate mocks",
            messageFormat: "Couldn't generate the mocks for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);
        private static readonly DiagnosticDescriptor NoTypeDiagnostic = new DiagnosticDescriptor(
            id: "SFMK002",
            title: "Only classes can be the target of the InjectMocksAttribute",
            messageFormat: "Incorrect type found for object '{0}'.  Expected IdentifierNameSyntax, but found {1}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);
        private static readonly DiagnosticDescriptor MissingDependencyDiagnostic = new DiagnosticDescriptor(
            id: "SFMK003",
            title: "A required dependency has not been found",
            messageFormat: "{1} is required in {0}, but it has not been found",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.UnitTestMocks.MSTest/Documentation/RuleDocumentation.md",
            isEnabledByDefault: true);

        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                //System.Diagnostics.Debugger.Launch();
            }
#endif 
            context.RegisterForSyntaxNotifications(() => new MockGeneratorReceiver());
        }

        private List<ParentNamespace> GetNamespace(SyntaxNode parent)
        {
            List<ParentNamespace> namespaces = new List<ParentNamespace>();

            while (parent != null)
            {
                if (parent is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                {
                    namespaces.Add(new ParentNamespace(namespaceDeclaration));
                }
                parent = parent.Parent;
            }

            namespaces.Reverse();
            return namespaces;
        }

        private string GetModifiers(ClassDeclarationSyntax classDeclarationSyntax)
        {
            string modifier = string.Empty;
            if (classDeclarationSyntax != null && classDeclarationSyntax.Modifiers != null && classDeclarationSyntax.Modifiers.Any())
            {
                modifier = string.Join(" ", classDeclarationSyntax.Modifiers.Select(p => p.Text));
            }
            if (modifier.IndexOf("partial") < 1)
            {
                modifier += " partial";
            }
            return modifier;
        }

        private List<(string className, string modifiers)> GetParentClasses(SyntaxNode parent)
        {
            var names = new List<(string className, string modifiers)>();

            while (parent != null && !(parent is NamespaceDeclarationSyntax))
            {
                if (parent is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    names.Add((classDeclarationSyntax.Identifier.Text, GetModifiers(classDeclarationSyntax)));
                }
                parent = parent.Parent;
            }

            return names;
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif

            var syntaxReceiver = (MockGeneratorReceiver)context.SyntaxContextReceiver;

            foreach (var targetClass in syntaxReceiver.ClassesToAugment)
            {
                try
                {
                    var semanticModel = context.Compilation.GetSemanticModel(targetClass.Key.SyntaxTree);

                    IdentifierNameSyntax targetType = targetClass
                        .Value
                        .ArgumentList
                        ?.Arguments
                        .Select(p => p.Expression)
                        .OfType<TypeOfExpressionSyntax>()
                        .Select(p => p.Type)
                        .OfType<IdentifierNameSyntax>()
                        .FirstOrDefault();

                    if (targetType is null)
                    {
                        var type = targetClass
                            .Value
                            .ArgumentList
                            ?.Arguments
                            .Select(p => p.Expression)
                            .OfType<TypeOfExpressionSyntax>()
                            .Select(p => p.Type)
                            .FirstOrDefault();

                        const string unknownText = "Unknown"; // TODO: Get from resx
                        context.ReportDiagnostic(Diagnostic.Create(NoTypeDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, type?.Kind().ToString() ?? unknownText));
                        continue;
                    }

                    bool skipUnderscore = false;
                    bool useLoose = false;

                    var options = context.AnalyzerConfigOptions.GetOptions(targetClass.Key.SyntaxTree);
                    if (options != null)
                    {
                        if (options.TryGetValue($"{RootConfig}skip_underscores", out string skipUnderscoreValue))
                        {
                            skipUnderscore = skipUnderscoreValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                        if (options.TryGetValue($"{RootConfig}use_loose", out string useLooseValue))
                        {
                            useLoose = useLooseValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                            // TODO: Log warning?  Also in SlowFox.constructors
                        }
                    }

                    // TODO 1a: Use this from SlowFox.Constructors?
                    //List<string> namespaces =
                    //    targetType.SyntaxTree
                    //    .GetRoot()
                    //    .DescendantNodes()
                    //    .OfType<UsingDirectiveSyntax>()
                    //    .Where(p => !p.Parent?.IsKind(SyntaxKind.NamespaceDeclaration) ?? true)
                    //    .Select(p => p.ToFullString())
                    //    .ToList();

                    List<ParentNamespace> namespaceValues = GetNamespace(targetType.Identifier.Parent);

                    var targetSymbol = context.Compilation.GetSemanticModel(targetType.SyntaxTree).GetSymbolInfo(targetType).Symbol as INamedTypeSymbol;

                    if (targetSymbol is null)
                    {
                        // TODO: Log warning?
                        continue;
                    }

                    // check that the users compilation references the expected library 
                    if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals("Moq", StringComparison.OrdinalIgnoreCase)))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(MissingDependencyDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, "Moq"));
                    }
                    if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals("Microsoft.VisualStudio.TestPlatform.TestFramework", StringComparison.OrdinalIgnoreCase)))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(MissingDependencyDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, "Microsoft.VisualStudio.TestPlatform.TestFramework"));
                    }

                    ImmutableArray<IParameterSymbol> parameters = targetSymbol.Constructors.FirstOrDefault()?.Parameters ?? new ImmutableArray<IParameterSymbol>();
                    List<(string parameterName, ITypeSymbol type)> types = parameters.Select(p => (p.Name, p.Type)).ToList();

                    // TODO 1b: Or this?
                    List<string> namespaces = new List<string>();

                    if (types != null && types.Any())
                    {
                        foreach (var type in types.Select(p => p.type).Union(new List<ITypeSymbol> { targetSymbol }))
                        {
                            var @namespace = type.ContainingNamespace.ToString();

                            namespaces.Add(@namespace);
                        }
                    }

                    //string namespaceValue = ((NamespaceDeclarationSyntax)targetClass.Key.Parent).Name.ToString();
                    //if (!string.IsNullOrEmpty(callingEntrypoint.ContainingNamespace.ContainingNamespace?.Name))
                    //{
                    //    namespaceValue = $"{callingEntrypoint.ContainingNamespace.ContainingNamespace.Name}.{namespaceValue}";
                    //}

                    List<(string className, string modifiers)> parentClasses = GetParentClasses(targetClass.Key.Identifier.Parent?.Parent);
                    string outputName = $"{targetClass.Key.Identifier.Text}";
                    if (parentClasses.Any())
                    {
                        parentClasses.Reverse();
                        outputName = string.Join("-", parentClasses.Select(p => p.className)) + "-" + outputName;
                    }

                    var fieldPrefix = skipUnderscore ? string.Empty : "_";
                    string mockBehavior = useLoose ? "Loose" : "Strict";

                    string methodSignature = $"private {targetSymbol.ContainingNamespace}.{targetSymbol.Name} Create()";
                    string methodBody = $"return new {targetSymbol.ContainingNamespace}.{targetSymbol.Name}({string.Join($", ", types.Select(p => $"{fieldPrefix}{p.parameterName}.Object"))});";

                    var newClass = new ClassWriter
                    {
                        UsingNamespaces = new List<string> { "using Microsoft.VisualStudio.TestTools.UnitTesting;", "using Moq;" },
                        Namespaces = namespaceValues,
                        ClassName = targetClass.Key.Identifier.Text,
                        Members = types.Select(p => $"private Mock<{p.type}> {fieldPrefix}{p.parameterName};").ToList(),
                        ParameterAssignments = types.Select(p => $"{fieldPrefix}{p.parameterName} = new Mock<{p.type}>(MockBehavior.{mockBehavior});").ToList(),
                        ParentClasses = parentClasses,
                        Modifier = GetModifiers(targetClass.Key),
                        MethodSignature = methodSignature,
                        MethodBody = methodBody
                    };

                    SourceText sourceText = SourceText.From(newClass.Render(), Encoding.UTF8);

                    context.AddSource($"{string.Join(".", namespaceValues.Select(p => p.NamespaceName))}.{outputName}.Generated.cs", sourceText);
                }
                catch (Exception ex)
                {
                    context.ReportDiagnostic(Diagnostic.Create(UnexpectedErrorDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, ex.Message, ex.StackTrace));
                }
            }
        }
    }
}
