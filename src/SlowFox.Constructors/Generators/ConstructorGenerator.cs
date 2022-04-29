﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Definitions;
using SlowFox.Constructors.Receivers;
using SlowFox.Constructors.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Constructors.Generators
{
    /// <summary>
    /// Source generator for generating constructors
    /// </summary>
    [Generator]
    public sealed partial class ConstructorGenerator : ISourceGenerator
    {
        private const string RootConfig = "slowfox_generation.constructors.";
        private static readonly DiagnosticDescriptor UnexpectedErrorDiagnostic = new DiagnosticDescriptor(
            id: "SFCT001",
            title: "Couldn't generate constructor",
            messageFormat: "Couldn't generate the constructor for object '{0}'.  {1} {2}.",
            category: "Design",
            DiagnosticSeverity.Warning,
            helpLinkUri: "https://github.com/Bungalow64/SlowFox/src/SlowFox.Constructors/Documentation/RuleDocumentation.md",
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
            context.RegisterForSyntaxNotifications(() => new ConstructorGeneratorReceiver());
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
                //System.Diagnostics.Debugger.Launch();
            }
#endif
            var syntaxReceiver = (ConstructorGeneratorReceiver)context.SyntaxContextReceiver;

            foreach (var targetClass in syntaxReceiver.ClassesToAugment)
            {
                try
                {
                    var semanticModel = context.Compilation.GetSemanticModel(targetClass.Key.SyntaxTree);

                    List<TypeSyntax> types = targetClass
                        .Value
                        .ArgumentList
                        ?.Arguments
                        .Select(p => p.Expression)
                        .OfType<TypeOfExpressionSyntax>()
                        .Select(p => p.Type)
                        .ToList();

                    if (!types?.Any() ?? true)
                    {
                        continue;
                    }

                    bool skipUnderscore = false;
                    bool includeNullCheck = false;

                    var options = context.AnalyzerConfigOptions.GetOptions(targetClass.Key.SyntaxTree);
                    if (options != null)
                    {
                        if (options.TryGetValue($"{RootConfig}skip_underscores", out string skipUnderscoreValue))
                        {
                            skipUnderscore = skipUnderscoreValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                        if (options.TryGetValue($"{RootConfig}include_nullcheck", out string includeNullcheckValue))
                        {
                            includeNullCheck = includeNullcheckValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                    }

                    List<string> namespaces =
                        targetClass.Key.SyntaxTree
                        .GetRoot()
                        .DescendantNodes()
                        .OfType<UsingDirectiveSyntax>()
                        .Where(p => !p.Parent?.IsKind(SyntaxKind.NamespaceDeclaration) ?? true)
                        .Select(p => p.ToFullString())
                        .ToList();

                    var baseParameters = new List<BaseParameter>();

                    if (!(targetClass.Key.BaseList is null))
                    {
                        foreach (var baseType in targetClass.Key.BaseList.Types)
                        {
                            var type = baseType.Type;

                            var actualType = semanticModel.GetTypeInfo(type).Type;

                            if (actualType is INamedTypeSymbol namedType)
                            {
                                var firstConstructor = namedType.InstanceConstructors.FirstOrDefault();

                                if (!(firstConstructor is null))
                                {
                                    var parameters = firstConstructor.Parameters;

                                    if (parameters.Any())
                                    {
                                        baseParameters = parameters.Select(p => new BaseParameter(p.Name, p.Type, false)).ToList();
                                        break;
                                    }
                                }
                            }
                        }
                    }


                    if (baseParameters.Any())
                    {
                        foreach (var type in types)
                        {
                            var actualType = semanticModel.GetTypeInfo(type).Type;

                            if (!(actualType is null))
                            {
                                var matches = baseParameters.FirstOrDefault(p => !p.AlreadyParameter && SymbolEqualityComparer.Default.Equals(p.Type, actualType));

                                if (!(matches is null))
                                {
                                    matches.AlreadyParameter = true;
                                }
                            }
                        }
                    }

                    List<ParentNamespace> namespaceValues = GetNamespace(targetClass.Key.Identifier.Parent);
                    List<(string className, string modifiers)> parentClasses = GetParentClasses(targetClass.Key.Identifier.Parent?.Parent);

                    var fieldPrefix = skipUnderscore ? string.Empty : "_";
                    var usedNames = new List<string>();
                    var names = types.Select(p => new TypeDetails(semanticModel, p, usedNames, fieldPrefix)).ToList();

                    string getConstructorFieldName(TypeDetails typeDetails)
                    {
                        if (string.IsNullOrEmpty(typeDetails.FieldPrefix))
                        {
                            return $"this.{typeDetails.MemberName}";
                        }
                        return typeDetails.MemberName;
                    }
                    string getNullCheck(TypeDetails typeDetails)
                    {
                        var nullCheck = "";
                        if (includeNullCheck && typeDetails.IsNullable)
                        {
                            nullCheck = $" ?? throw new System.ArgumentNullException(nameof({typeDetails.Name}))";
                        }
                        return nullCheck;
                    }

                    string outputName = $"{targetClass.Key.Identifier.Text}";
                    if (parentClasses.Any())
                    {
                        parentClasses.Reverse();
                        outputName = string.Join("-", parentClasses.Select(p => p.className)) + "-" + outputName;
                    }

                    var newClass = new ClassWriter
                    {
                        UsingNamespaces = namespaces,
                        Namespaces = namespaceValues,
                        ClassName = targetClass.Key.Identifier.Text,
                        Members = names.Select(p => $"private readonly {p.TypeName} {p.MemberName};").ToList(),
                        Parameters = names.Select(p => $"{p.TypeName} {p.InputName}").ToList(),
                        ParameterAssignments = names.Select(p => $"{getConstructorFieldName(p)} = {p.InputName}{getNullCheck(p)};").ToList(),
                        ParentClasses = parentClasses,
                        Modifier = GetModifiers(targetClass.Key),
                        BaseParameters = baseParameters.Select(p => (p.Type.ToString(), p.Name, p.AlreadyParameter)).ToList()
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

        private class BaseParameter
        {
            public string Name { get; set; }
            public ITypeSymbol Type { get; set; }
            public bool AlreadyParameter { get; set; }

            public BaseParameter(string name, ITypeSymbol type, bool alreadyParameter)
            {
                Name = name;
                Type = type;
                AlreadyParameter = alreadyParameter;
            }
        }
    }
}