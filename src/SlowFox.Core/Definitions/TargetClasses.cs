﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SlowFox.Core.Configuration.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Core.Definitions
{
    public class TargetClasses
    {
        public List<TargetClass> Classes { get; private set; } = new List<TargetClass>();

        public void Add(ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attributeSyntax, ITypeSymbol baseType)
        {
            Classes.Add(new TargetClass(classDeclarationSyntax, attributeSyntax, baseType));
        }
        public TargetClass Find(GeneratorExecutionContext context, ITypeSymbol type)
        {
            if (type is null)
            {
                return null;
            }
            return Classes.FirstOrDefault(p => p.Matches(context, type));
        }

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