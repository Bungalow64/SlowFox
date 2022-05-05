using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Constructors.Definitions
{
    internal class TargetClasses
    {
        internal List<TargetClass> Classes { get; private set; } = new List<TargetClass>();

        internal void Add(ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attributeSyntax, ITypeSymbol baseType)
        {
            Classes.Add(new TargetClass(classDeclarationSyntax, attributeSyntax, baseType));
        }
        internal TargetClass Find(GeneratorExecutionContext context, ITypeSymbol type)
        {
            if (type is null)
            {
                return null;
            }
            return Classes.FirstOrDefault(p => p.Matches(context, type));
        }

        internal void Process(DiagnosticDescriptor UnexpectedErrorDiagnostic, GeneratorExecutionContext context)
        {
            foreach (var @class in Classes)
            {
                try
                {
                    @class.Process(context, this);

                    if (!(@class.GeneratedClass is null))
                    {
                        SourceText sourceText = SourceText.From(@class.GeneratedClass.Render(), Encoding.UTF8);
                        context.AddSource(@class.GeneratedClass.OutputName, sourceText);
                    }
                }
                catch (Exception ex)
                {
                    context.ReportDiagnostic(Diagnostic.Create(UnexpectedErrorDiagnostic, @class.AttributeSyntax.GetLocation(), @class.ClassDeclarationSyntax.Identifier.Value, ex.Message, ex.StackTrace));
                }
            }
        }
    }

    internal class TargetClass
    {
        internal ClassDeclarationSyntax ClassDeclarationSyntax { get; set; }
        internal AttributeSyntax AttributeSyntax { get; set; }
        internal ITypeSymbol BaseType { get; set; }
        internal ClassWriter GeneratedClass { get; set; }
        internal TargetClass BaseClass { get; set; }
        internal bool IsPending { get; set; } = true;
        internal bool IsProcessing { get; set; }
        internal bool HasBase => !(BaseType is null);

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

        internal void Process(GeneratorExecutionContext context, TargetClasses classes)
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
                    BaseClass.Process(context, classes);
                }
            }

            GeneratedClass = DependencyReader.Read(context, ClassDeclarationSyntax, AttributeSyntax, BaseClass);
            IsPending = false;
        }
    }
}
