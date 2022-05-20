using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.GeneratorLogic.Constructor.Logic;

namespace SlowFox.Core.Definitions
{
    public class TargetClass
    {
        internal ClassDeclarationSyntax ClassDeclarationSyntax { get; set; }
        internal AttributeSyntax AttributeSyntax { get; set; }
        internal ITypeSymbol BaseType { get; set; }
        public ClassWriter GeneratedClass { get; set; }
        internal TargetClass BaseClass { get; set; }
        internal bool IsPending { get; set; } = true;
        internal bool IsProcessing { get; set; }
        internal bool HasBase => !(BaseType is null);

        public bool Matches(string fullTypeName)
        {
            if (string.IsNullOrWhiteSpace(fullTypeName))
            {
                return false;
            }
            return GeneratedClass?.FullTypeName == $"{fullTypeName}";
        }

        internal bool Matches(GeneratorExecutionContext context, ITypeSymbol type)
        {
            var semanticModel = context.Compilation.GetSemanticModel(ClassDeclarationSyntax.SyntaxTree);

            var currentType = semanticModel.GetDeclaredSymbol(ClassDeclarationSyntax);

            return SymbolEqualityComparer.Default.Equals(currentType, type);
        }

        internal TargetClass(ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attributeSyntax, ITypeSymbol baseType)
        {
            ClassDeclarationSyntax = classDeclarationSyntax;
            AttributeSyntax = attributeSyntax;
            BaseType = baseType;
        }

        internal void Process(GeneratorExecutionContext context, IDiagnosticGenerator diagnosticGenerator, TargetClasses classes)
        {
            if (!IsPending || IsProcessing)
            {
                return;
            }

            if (HasBase)
            {
                BaseClass = classes.Find(context, BaseType);
                if (!(BaseClass is null))
                {
                    IsProcessing = true;
                    BaseClass.Process(context, diagnosticGenerator, classes);
                }
            }

            GeneratedClass = DependencyReader.Read(context, diagnosticGenerator, ClassDeclarationSyntax, AttributeSyntax, BaseClass);
            IsPending = false;
        }
    }
}
