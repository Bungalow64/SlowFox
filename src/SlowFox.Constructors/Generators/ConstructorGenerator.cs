using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Constructors.Generators
{
    [Generator]
    public sealed class ConstructorGenerator : ISourceGenerator
    {
        private const string InjectableClassAttributeName1 = "SlowFox.InjectDependenciesAttribute";
        private const string InjectableClassAttributeName2 = "SlowFox.InjectDependencies";
        private const string InjectableClassAttributeName3 = "InjectDependencies";

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

                List<string> namespaces = new List<string>();

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


                if (types != null && types.Any())
                {
                    //foreach (var type in types)
                    //{
                    //    var @namespace = context
                    //        .Compilation
                    //        ?.GetSemanticModel(type.SyntaxTree)
                    //        ?.GetSymbolInfo(type)
                    //        .Symbol
                    //        ?.ContainingNamespace
                    //        ?.ToString();

                    //    namespaces.Add(@namespace);
                    //}
                    namespaces = targetClass.Key.SyntaxTree.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>().Select(p => p.ToFullString()).ToList();
                }

                string namespaceValue = GetNamespace(targetClass.Key.Identifier.Parent);

                string namespaceList = string.Join("", namespaces.Distinct().Except(new List<string> { namespaceValue }).Select(p => $"{p}"));
                if (namespaceList.Length > 0)
                {
                    namespaceList += Environment.NewLine;
                }
                string propertyList = string.Empty;
                string ctor = string.Empty;


                if (types != null)
                {
                    var fieldPrefix = skipUnderscore ? string.Empty : "_";
                    var usedNames = new List<string>();
                    var names = types.Select(p => new TypeDetails(semanticModel, p, usedNames, fieldPrefix)).ToList();


                    //var names = typeNames.Select(p => new { TypeName = ((IdentifierNameSyntax)p).Identifier.Text, Name = NameGenerator.GetName(((IdentifierNameSyntax)p).Identifier.Text, usedNames) }).ToList();

                    propertyList = string.Join(Environment.NewLine, names.Select(p => $"        private readonly {p.TypeName} {p.MemberName};"));

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
                    ctor = $@"        public {targetClass.Key.Identifier}({string.Join(", ", names.Select(p => $"{p.TypeName} {p.InputName}"))})
        {{
{string.Join(Environment.NewLine, names.Select(p => $"            {getConstructorFieldName(p)} = {p.InputName}{getNullCheck(p)};"))}
        }}";
                }

                if (propertyList.Length > 0)
                {
                    propertyList += Environment.NewLine;
                    propertyList += Environment.NewLine;
                }

                SourceText sourceText = SourceText.From($@"{namespaceList}namespace {namespaceValue}
{{
    public partial class {targetClass.Key.Identifier}
    {{
{propertyList}{ctor}
    }}
}}", Encoding.UTF8);
                context.AddSource($"{namespaceValue}.{targetClass.Key.Identifier.Text}.Generated.cs", sourceText);
            }
        }
        class TypeDetails
        {
            public string TypeName { get; set; }
            public string Name { get; set; }
            public bool IsNullable { get; set; }
            public bool RequiresPrefix => TypeName.Replace("?", "") == Name;
            public string InputName => RequiresPrefix ? $"@{Name}" : Name;
            public string FieldPrefix { get; set; }
            public string MemberName
            {
                get
                {
                    string expected = $"{FieldPrefix}{Name}";
                    if (expected == TypeName.Replace("?", ""))
                    {
                        return $"@{expected}";
                    }
                    return expected;
                }
            }

            public TypeDetails(SemanticModel semanticModel, TypeSyntax typeSyntax, List<string> existingNames, string fieldPrefix)
            {
                FieldPrefix = fieldPrefix;

                switch (typeSyntax)
                {
                    case IdentifierNameSyntax identifierNameSyntax:
                        TypeName = identifierNameSyntax.Identifier.Text;
                        Name = NameGenerator.GetName(TypeName, existingNames);
                        break;
                    case NullableTypeSyntax nullableTypeSyntax:
                        TypeName = nullableTypeSyntax.GetText().ToString();
                        Name = NameGenerator.GetName(TypeName, existingNames);
                        break;
                    case QualifiedNameSyntax qualifiedNameSyntax:
                        TypeName = qualifiedNameSyntax.GetText().ToString();
                        Name = NameGenerator.GetName(TypeName, existingNames);
                        break;
                    default:
                        TypeName = typeSyntax.ToString();
                        Name = typeSyntax.ToString();
                        break;
                        //case AliasQualifiedNameSyntax aliasQualifiedNameSyntax:
                        //    TypeName = aliasQualifiedNameSyntax.GetText().ToString();
                        //    break;
                }
                var type = semanticModel.GetTypeInfo(typeSyntax).Type;

                if (type != null)
                {
                    IsNullable = type.IsReferenceType;
                    //if (type is INamedTypeSymbol namedTypeSymbol)
                    //{
                    //    IsNullable = type.IsReferenceType || namedTypeSymbol.ConstructedFrom?.ToString() == "System.Nullable<T>";
                    //}
                }
            }
        }

        class ConstructorGeneratorReceiver : ISyntaxContextReceiver
        {
            public List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>> ClassesToAugment { get; private set; } = new List<KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is ClassDeclarationSyntax cds)
                {
                    // TODO: Remove
                    var allChildren = cds.DescendantNodes().OfType<AttributeSyntax>().SelectMany(p => p.DescendantTokens()).Where(dt => dt.IsKind(SyntaxKind.IdentifierToken)).ToList();

                    bool matches(string type)
                    {
                        if (string.IsNullOrEmpty(type))
                        {
                            return false;
                        }
                        return type.Equals(InjectableClassAttributeName1) || type.Equals(InjectableClassAttributeName2) || type.Equals(InjectableClassAttributeName3);
                    }

                    var attribute = cds
                        .DescendantNodes()
                        .OfType<AttributeSyntax>()
                        .Where(p => p.DescendantTokens().Any(dt => dt.IsKind(SyntaxKind.IdentifierToken) && matches(context.SemanticModel.GetTypeInfo(dt.Parent).Type?.ToString())))
                        .FirstOrDefault();

                    if (attribute != null)
                    {
                        ClassesToAugment.Add(new KeyValuePair<ClassDeclarationSyntax, AttributeSyntax>(cds, attribute));
                    }
                }
            }
        }
    }
}