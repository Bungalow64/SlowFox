using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.UnitTestMocks.MSTest.Configuration;
using SlowFox.UnitTestMocks.MSTest.Definitions;
using SlowFox.UnitTestMocks.MSTest.Extensions;
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
        private const string NamespaceMoq = "Moq";
        private const string NamespaceMSTest = "Microsoft.VisualStudio.TestPlatform.TestFramework";
        private const string UnknownText = "Unknown";
        private const string AttributeNamespace = "SlowFox";
        private const string ExcludeMocksAttributeName = "ExcludeMocks";

        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MockGeneratorReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                //System.Diagnostics.Debugger.Launch();
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

                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.NoTypeDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, type?.Kind().ToString() ?? UnknownText));
                        continue;
                    }

                    var excludedRawTypes = targetClass
                        .Key
                        .FindAttributes(semanticModel, AttributeNamespace, ExcludeMocksAttributeName)
                        .Select(p => p.ArgumentList)
                        .SelectMany(p => p.Arguments)
                        .Select(p => p.Expression)
                        .ToList();

                    var excludedTypes = excludedRawTypes
                        .OfType<TypeOfExpressionSyntax>()
                        .Select(p => semanticModel.GetTypeInfo((IdentifierNameSyntax)p.Type).Type)
                        .Where(p => !(p is null))
                        .ToList();

                    excludedTypes
                        .AddRange(excludedRawTypes
                        .OfType<ArrayCreationExpressionSyntax>()
                        .SelectMany(p => p.Initializer.Expressions.OfType<TypeOfExpressionSyntax>())
                        .Select(p => semanticModel.GetTypeInfo((IdentifierNameSyntax)p.Type).Type)
                        .Where(p => !(p is null)));

                    excludedTypes
                        .AddRange(excludedRawTypes
                        .OfType<ImplicitArrayCreationExpressionSyntax>()
                        .SelectMany(p => p.Initializer.Expressions.OfType<TypeOfExpressionSyntax>())
                        .Select(p => semanticModel.GetTypeInfo((IdentifierNameSyntax)p.Type).Type)
                        .Where(p => !(p is null)));

                    excludedTypes
                        .AddRange(excludedRawTypes
                        .OfType<ImplicitStackAllocArrayCreationExpressionSyntax>()
                        .SelectMany(p => p.Initializer.Expressions.OfType<TypeOfExpressionSyntax>())
                        .Select(p => semanticModel.GetTypeInfo((IdentifierNameSyntax)p.Type).Type)
                        .Where(p => !(p is null)));

                    List<ParentNamespace> namespaceValues = targetType.Identifier.Parent.GetNamespace();

                    if (!(context.Compilation.GetSemanticModel(targetType.SyntaxTree).GetSymbolInfo(targetType).Symbol is INamedTypeSymbol targetSymbol))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.NoTypeDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, targetType?.Kind().ToString() ?? UnknownText));
                        continue;
                    }

                    // check that the users compilation references the expected library 
                    if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals(NamespaceMoq, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.MissingDependencyDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, NamespaceMoq));
                    }
                    if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals(NamespaceMSTest, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.MissingDependencyDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, NamespaceMSTest));
                    }

                    ImmutableArray<IParameterSymbol> parameters = targetSymbol.InstanceConstructors.FirstOrDefault()?.Parameters ?? new ImmutableArray<IParameterSymbol>();
                    List<(string parameterName, ITypeSymbol type)> types = parameters.Select(p => (p.Name, p.Type)).ToList();

                    List<(string className, string modifiers)> parentClasses = targetClass.Key.Identifier.Parent?.Parent.GetParentClasses();
                    string outputName = $"{targetClass.Key.Identifier.Text}";
                    if (parentClasses.Any())
                    {
                        parentClasses.Reverse();
                        outputName = string.Join("-", parentClasses.Select(p => p.className)) + "-" + outputName;
                    }

                    var config = new CustomConfiguration(context, targetClass);

                    var fieldPrefix = config.SkipUnderscore ? string.Empty : "_";
                    string mockBehavior = config.UseLoose ? "Loose" : "Strict";

                    bool isToBeMocked(ITypeSymbol type)
                    {
                        return type.CanBeMocked() && !excludedTypes.Contains(type);
                    }

                    string methodSignature = $"private {targetSymbol.ContainingNamespace}.{targetSymbol.Name} Create({string.Join(", ", types.Where(p => !isToBeMocked(p.type)).Select(p => $"{p.type} {p.parameterName}"))})";
                    string methodBody = $"return new {targetSymbol.ContainingNamespace}.{targetSymbol.Name}({string.Join($", ", types.Select(p => isToBeMocked(p.type) ? $"{fieldPrefix}{p.parameterName}.Object" : $"{p.parameterName}"))});";

                    var newClass = new ClassWriter
                    {
                        UsingNamespaces = new List<string> { "using Microsoft.VisualStudio.TestTools.UnitTesting;", "using Moq;" },
                        Namespaces = namespaceValues,
                        ClassName = targetClass.Key.Identifier.Text,
                        Members = types.Where(p => isToBeMocked(p.type)).Select(p => $"private Mock<{p.type}> {fieldPrefix}{p.parameterName};").ToList(),
                        ParameterAssignments = types.Where(p => isToBeMocked(p.type)).Select(p => $"{fieldPrefix}{p.parameterName} = new Mock<{p.type}>(MockBehavior.{mockBehavior});").ToList(),
                        ParentClasses = parentClasses,
                        Modifier = targetClass.Key.GetModifiers(),
                        MethodSignature = methodSignature,
                        MethodBody = methodBody
                    };

                    SourceText sourceText = SourceText.From(newClass.Render(), Encoding.UTF8);

                    context.AddSource($"{string.Join(".", namespaceValues.Select(p => p.NamespaceName))}.{outputName}.Generated.cs", sourceText);
                }
                catch (Exception ex)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.UnexpectedErrorDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, ex.Message, ex.StackTrace));
                }
            }
        }
    }
}
