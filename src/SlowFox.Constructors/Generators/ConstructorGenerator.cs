﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Generators.Definitions;
using SlowFox.Constructors.Generators.Receivers;
using SlowFox.Constructors.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Constructors.Generators
{
    [Generator]
    public sealed partial class ConstructorGenerator : ISourceGenerator
    {

        private const string RootConfig = "slowfox_generation.constructors.";

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

        private string GetNamespace(SyntaxNode parent)
        {
            while (parent != null && !(parent is NamespaceDeclarationSyntax))
            {
                parent = parent.Parent;
            }

            return ((NamespaceDeclarationSyntax)parent).Name.ToString();
        }

        private string GetModifiers(ClassDeclarationSyntax classDeclarationSyntax)
        {
            string modifier = string.Empty;
            if (classDeclarationSyntax.Modifiers != null && classDeclarationSyntax.Modifiers.Any())
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
                    return;
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

                (string namespaceText, bool withinNamespace) getNamespace(UsingDirectiveSyntax node) 
                {
                    return (node.ToFullString(), node.Parent?.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.NamespaceDeclaration) ?? false);
                }

                List<(string namespaceText, bool withinNamespace)> namespaces = 
                    targetClass.Key.SyntaxTree.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>().Select(p => getNamespace(p)).ToList();

                string namespaceValue = GetNamespace(targetClass.Key.Identifier.Parent);
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
                    Namespace = namespaceValue,
                    ClassName = targetClass.Key.Identifier.Text,
                    Members = names.Select(p => $"private readonly {p.TypeName} {p.MemberName};").ToList(),
                    Parameters = names.Select(p => $"{p.TypeName} {p.InputName}").ToList(),
                    ParameterAssignments = names.Select(p => $"{getConstructorFieldName(p)} = {p.InputName}{getNullCheck(p)};").ToList(),
                    ParentClasses = parentClasses,
                    Modifier = GetModifiers(targetClass.Key)
                };

                SourceText sourceText = SourceText.From(newClass.Render(), Encoding.UTF8);
                context.AddSource($"{namespaceValue}.{outputName}.Generated.cs", sourceText);
            }
        }
    }
}