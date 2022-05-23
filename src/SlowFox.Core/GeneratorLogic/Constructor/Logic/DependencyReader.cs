using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.Definitions;
using SlowFox.Core.Extensions;
using SlowFox.Core.GeneratorLogic.Constructor.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Core.GeneratorLogic.Constructor.Logic
{
    /// <summary>
    /// Detects details of all dependencies
    /// </summary>
    public static class DependencyReader
    {
        private const string RootConfig = "slowfox_generation.constructors.";
        private const string InjectableClassAttributeName1 = "SlowFox.InjectDependenciesAttribute";
        private const string InjectableClassAttributeName2 = "SlowFox.InjectDependencies";
        private const string InjectableClassAttributeName3 = "InjectDependencies";

        /// <summary>
        /// Reads and processes the dependencies
        /// </summary>
        /// <param name="context"></param>
        /// <param name="diagnosticGenerator"></param>
        /// <param name="classDeclaration"></param>
        /// <param name="attribute"></param>
        /// <param name="baseTargetClass"></param>
        /// <returns></returns>
        public static ClassWriter Read(GeneratorExecutionContext context, IDiagnosticGenerator diagnosticGenerator, ClassDeclarationSyntax classDeclaration, AttributeSyntax attribute, TargetClass baseTargetClass)
        {
            var semanticModel = context.Compilation.GetSemanticModel(classDeclaration.SyntaxTree);

            var baseParameters = new List<BaseParameter>();

            if (!(baseTargetClass is null))
            {
                ClassDeclarationSyntax baseClass = baseTargetClass.ClassDeclarationSyntax;
                AttributeSyntax baseClassAttribute = baseTargetClass.AttributeSyntax;

                if (!(baseClass is null) && !(baseTargetClass.GeneratedClass is null))
                {
                    List<TypeDetails> parameterTypes = baseTargetClass.GeneratedClass.ParameterTypes;

                    baseParameters = baseTargetClass.GeneratedClass.BaseParameters.Select(p => p.Clone()).Union(parameterTypes.Select(p => new BaseParameter(p.Name, p.Type, false))).ToList();
                }
            }
            else if (!(classDeclaration.BaseList is null))
            {
                foreach (var baseType in classDeclaration.BaseList.Types)
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

            List<TypeSyntax> types = attribute
                .ArgumentList
                ?.Arguments
                .Select(p => p.Expression)
                .OfType<TypeOfExpressionSyntax>()
                .Select(p => p.Type)
                .ToList() ?? new List<TypeSyntax>();

            if (!types.Any() && !baseParameters.Any())
            {
                return null;
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

            var config = new CustomConfiguration(context, RootConfig, classDeclaration, attribute, diagnosticGenerator);

            List<string> namespaces =
                classDeclaration.SyntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Where(p => !p.Parent?.IsKind(SyntaxKind.NamespaceDeclaration) ?? true)
                .Select(p => p.ToFullString())
                .ToList();

            List<ParentNamespace> namespaceValues = classDeclaration.Identifier.Parent.GetNamespace();
            List<(string className, string modifiers)> parentClasses = classDeclaration.Identifier.Parent?.Parent.GetParentClasses();

            var fieldPrefix = config.SkipUnderscore ? string.Empty : "_";
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
                if (config.IncludeNullCheck && typeDetails.IsNullable)
                {
                    nullCheck = $" ?? throw new System.ArgumentNullException(nameof({typeDetails.Name}))";
                }
                return nullCheck;
            }

            if (parentClasses.Any())
            {
                parentClasses.Reverse();
            }

            string GenerateOutputName(string separator = "-")
            {
                string outputName = $"{classDeclaration.Identifier.Text}";
                if (parentClasses.Any())
                {
                    outputName = string.Join(separator, parentClasses.Select(p => p.className)) + separator + outputName;
                }
                return outputName;
            }

            var newClass = new ClassWriter
            {
                UsingNamespaces = namespaces,
                Namespaces = namespaceValues,
                ClassName = classDeclaration.Identifier.Text,
                Members = names.Select(p => $"private readonly {p.TypeName} {p.MemberName};").ToList(),
                Parameters = names.Select(p => $"{p.TypeName} {p.InputName}").ToList(),
                ParameterAssignments = names.Select(p => $"{getConstructorFieldName(p)} = {p.InputName}{getNullCheck(p)};").ToList(),
                ParentClasses = parentClasses,
                Modifier = classDeclaration.GetModifiers(),
                BaseParameters = baseParameters,
                GenerateProtectedConstructor = classDeclaration.Modifiers.Any(p => p.Value?.Equals("abstract") ?? false),
                OutputName = $"{string.Join(".", namespaceValues.Select(p => p.NamespaceName))}.{GenerateOutputName()}.Generated.cs",
                ParameterTypes = names
            };

            return newClass;
        }

        /// <summary>
        /// Finds the attribute for the class
        /// </summary>
        /// <param name="semanticModel"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static (ClassDeclarationSyntax, AttributeSyntax, ITypeSymbol) FindAttribute(SemanticModel semanticModel, SyntaxNode node)
        {
            bool matches(string type)
            {
                if (string.IsNullOrEmpty(type))
                {
                    return false;
                }
                return type.Equals(InjectableClassAttributeName1) || type.Equals(InjectableClassAttributeName2) || type.Equals(InjectableClassAttributeName3);
            }

            if (node is ClassDeclarationSyntax cds)
            {
                var attribute = cds
                    .AttributeLists
                    .SelectMany(p => p.Attributes)
                    .Where(p => p.DescendantTokens().Any(dt => dt.IsKind(SyntaxKind.IdentifierToken) && dt.Parent != null && matches(semanticModel.GetTypeInfo(dt.Parent).Type?.ToString())))
                    .FirstOrDefault();

                ITypeSymbol actualBaseType = null;
                if (!(cds.BaseList is null))
                {
                    foreach (var baseType in cds.BaseList.Types)
                    {
                        var actualType = semanticModel.GetTypeInfo(baseType.Type).Type;
                        if (actualType?.TypeKind == TypeKind.Class)
                        {
                            actualBaseType = actualType;
                            break;
                        }
                    }
                }

                if (attribute != null)
                {
                    return (cds, attribute, actualBaseType);
                }
            }

            return (null, null, null);
        }
    }
}
