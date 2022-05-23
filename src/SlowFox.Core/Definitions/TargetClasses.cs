using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Core.Configuration.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Core.Definitions
{
    /// <summary>
    /// The definition of a collection of classes
    /// </summary>
    public class TargetClasses
    {
        /// <summary>
        /// The list of classes
        /// </summary>
        public List<TargetClass> Classes { get; private set; } = new List<TargetClass>();

        /// <summary>
        /// Adds a new class
        /// </summary>
        /// <param name="classDeclarationSyntax"></param>
        /// <param name="attributeSyntax"></param>
        /// <param name="baseType"></param>
        public void Add(ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attributeSyntax, ITypeSymbol baseType)
        {
            Classes.Add(new TargetClass(classDeclarationSyntax, attributeSyntax, baseType));
        }

        /// <summary>
        /// Finds a specific type within the list of classes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public TargetClass Find(GeneratorExecutionContext context, ITypeSymbol type)
        {
            if (type is null)
            {
                return null;
            }
            return Classes.FirstOrDefault(p => p.Matches(context, type));
        }

        /// <summary>
        /// Processes each class
        /// </summary>
        /// <param name="diagnosticGenerator"></param>
        /// <param name="context"></param>
        /// <param name="skipSourceGeneration"></param>
        public void Process(IDiagnosticGenerator diagnosticGenerator, GeneratorExecutionContext context, bool skipSourceGeneration = false)
        {
            foreach (var @class in Classes)
            {
                try
                {
                    @class.Process(context, diagnosticGenerator, this);

                    if (!skipSourceGeneration && !(@class.GeneratedClass is null))
                    {
                        SourceText sourceText = SourceText.From(@class.GeneratedClass.Render(), Encoding.UTF8);
                        context.AddSource(@class.GeneratedClass.OutputName, sourceText);
                    }
                }
                catch (Exception ex)
                {
                    if (diagnosticGenerator.HasUnexpectedErrorDiagnostic)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(diagnosticGenerator.UnexpectedErrorDiagnostic, @class.AttributeSyntax.GetLocation(), @class.ClassDeclarationSyntax.Identifier.Value, ex.Message, ex.StackTrace));
                    }
                }
            }
        }
    }
}
