using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Configuration.Abstract;
using SlowFox.Core.GeneratorLogic.Constructor.Logic;

namespace SlowFox.Core.Definitions
{
    /// <summary>
    /// Handles the definition of a target glass
    /// </summary>
    public class TargetClass
    {
        internal ClassDeclarationSyntax ClassDeclarationSyntax { get; set; }
        internal AttributeSyntax AttributeSyntax { get; set; }
        internal ITypeSymbol BaseType { get; set; }
        internal TargetClass BaseClass { get; set; }
        internal bool IsPending { get; set; } = true;
        internal bool IsProcessing { get; set; }
        internal bool HasBase => !(BaseType is null);
        /// <summary>
        /// The generated class
        /// </summary>
        public ClassWriter GeneratedClass { get; set; }

        /// <summary>
        /// Whether the supplied type matches the type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal bool Matches(GeneratorExecutionContext context, ITypeSymbol type)
        {
            if (type is null)
            {
                return false;
            }

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
