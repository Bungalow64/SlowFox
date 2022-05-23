using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Core.Configuration;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.Definitions;
using SlowFox.Core.Extensions;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Configuration;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Logic;
using SlowFox.Core.GeneratorLogic.UnitTestMocks.Receivers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SlowFox.Core.GeneratorLogic.UnitTestMocks.Generators
{
    /// <summary>
    /// The base mock generator for generating mock objects
    /// </summary>
    public abstract class BaseMockGenerator : ISourceGenerator
    {
        private const string NamespaceMoq = "Moq";
        private const string UnknownText = "Unknown";
        private const string AttributeNamespace = "SlowFox";
        private const string ExcludeMocksAttributeName = "ExcludeMocks";
        private readonly IDiagnosticGenerator noDiagnostics = new EmptyDiagnosticGenerator();

        /// <summary>
        /// The name of the test framework filename
        /// </summary>
        protected abstract string DependencyFilename { get; }
        /// <summary>
        /// The using declaration that is to be included in the generated file
        /// </summary>
        protected abstract string CustomUsing { get; }
        /// <summary>
        /// The root config value
        /// </summary>
        protected abstract string RootConfig { get; }
        /// <summary>
        /// The name of the attribute to be used on the initialisation method (if any)
        /// </summary>
        protected abstract string InitAttribute { get; }
        /// <summary>
        /// The name of the method to be used for initialisation (if any)
        /// </summary>
        protected abstract string InitMethodName { get; }
        /// <summary>
        /// The diagnostics for the generator
        /// </summary>
        protected abstract IDiagnosticGenerator Diagnostics { get; }

        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MockGeneratorReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is MockGeneratorReceiver syntaxReceiver))
            {
                return;
            }

            syntaxReceiver.ClassesToInject.Process(noDiagnostics, context, skipSourceGeneration: true);

            foreach (var targetClass in syntaxReceiver.ClassesToAugment)
            {
                try
                {
                    var semanticModel = context.Compilation.GetSemanticModel(targetClass.Key.SyntaxTree);

                    TypeSyntax targetType = targetClass
                        .Value
                        .ArgumentList
                        ?.Arguments
                        .Select(p => p.Expression)
                        .OfType<TypeOfExpressionSyntax>()
                        .Select(p => p.Type)
                        .FirstOrDefault();


                    List<ParentNamespace> namespaceValues;
                    string typeName;

                    if (targetType is IdentifierNameSyntax identifierNameSyntax)
                    {
                        namespaceValues = identifierNameSyntax.GetNamespace();
                        typeName = identifierNameSyntax.Identifier.ToString();
                    }
                    else if (targetType is QualifiedNameSyntax qualifiedNameSyntax)
                    {
                        namespaceValues = qualifiedNameSyntax.GetNamespace();
                        typeName = qualifiedNameSyntax.Right.Identifier.ToString();
                    }
                    else
                    {
                        var type = targetClass
                            .Value
                            .ArgumentList
                            ?.Arguments
                            .Select(p => p.Expression)
                            .OfType<TypeOfExpressionSyntax>()
                            .Select(p => p.Type)
                            .FirstOrDefault();

                        if (!(type is null))
                        {
                            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.NoTypeDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, type.Kind().ToString()));
                        }

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
                    if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals(DependencyFilename, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.MissingDependencyDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, DependencyFilename));
                    }

                    List<(string parameterName, ITypeSymbol type)> types;

                    ImmutableArray<IParameterSymbol> parameters = targetSymbol.InstanceConstructors.FirstOrDefault()?.Parameters ?? new ImmutableArray<IParameterSymbol>();
                    types = parameters.Select(p => (p.Name, p.Type)).ToList();

                    if (!types.Any())
                    {
                        var injectClass = syntaxReceiver.ClassesToInject.Classes.FirstOrDefault(p => p.Matches(context, semanticModel.GetTypeInfo(targetType).Type));
                        if (!(injectClass is null))
                        {
                            types = injectClass.GeneratedClass.ParameterTypes.Select(p => (p.Name, p.Type)).ToList();
                        }
                    }

                    List<(string className, string modifiers)> parentClasses = targetClass.Key.Identifier.Parent?.Parent.GetParentClasses();

                    if (parentClasses.Any())
                    {
                        parentClasses.Reverse();
                    }

                    string GenerateOutputName(string separator = "-")
                    {
                        string outputName = $"{targetClass.Key.Identifier.Text}";
                        if (parentClasses.Any())
                        {
                            outputName = string.Join(separator, parentClasses.Select(p => p.className)) + separator + outputName;
                        }
                        return outputName;
                    }

                    var config = new CustomConfiguration(context, RootConfig, targetClass, Diagnostics);

                    var fieldPrefix = config.SkipUnderscore ? string.Empty : "_";
                    string mockBehavior = config.UseLoose ? "Loose" : "Strict";

                    bool isToBeMocked(ITypeSymbol type)
                    {
                        return type.CanBeMocked() && !excludedTypes.Contains(type);
                    }

                    string methodSignature = $"private {targetSymbol} Create({string.Join(", ", types.Where(p => !isToBeMocked(p.type)).Select(p => $"{p.type} {p.parameterName}"))})";
                    string methodBody = $"return new {targetSymbol}({string.Join($", ", types.Select(p => isToBeMocked(p.type) ? $"{fieldPrefix}{p.parameterName}.Object" : $"{p.parameterName}"))});";

                    var newClass = new ClassWriter
                    {
                        UsingNamespaces = new List<string> { CustomUsing, "using Moq;" },
                        Namespaces = namespaceValues,
                        ClassName = GenerateOutputName("."),
                        Members = types.Where(p => isToBeMocked(p.type)).Select(p => $"private Mock<{p.type}> {fieldPrefix}{p.parameterName};").ToList(),
                        Assignments = types.Where(p => isToBeMocked(p.type)).Select(p => $"{fieldPrefix}{p.parameterName} = new Mock<{p.type}>(MockBehavior.{mockBehavior});").ToList(),
                        ParentClasses = parentClasses,
                        Modifier = targetClass.Key.GetModifiers(),
                        MethodSignature = methodSignature,
                        MethodBody = methodBody,
                        InitAttribute = InitAttribute,
                        InitMethodName = InitMethodName
                    };

                    SourceText sourceText = SourceText.From(newClass.Render(), Encoding.UTF8);

                    context.AddSource($"{string.Join(".", namespaceValues.Select(p => p.NamespaceName))}.{GenerateOutputName()}.Generated.cs", sourceText);
                }
                catch (Exception ex)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.UnexpectedErrorDiagnostic, targetClass.Value.GetLocation(), targetClass.Key.Identifier.Value, ex.Message, ex.StackTrace));
                }
            }
        }
    }
}
