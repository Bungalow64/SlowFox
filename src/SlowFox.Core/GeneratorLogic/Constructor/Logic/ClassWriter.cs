﻿using SlowFox.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Core.GeneratorLogic.Constructor.Logic
{
    /// <summary>
    /// Builds the contents of the class
    /// </summary>
    public class ClassWriter
    {
        /// <summary>
        /// The name of the class
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// The list of namespaces that the class belongs within
        /// </summary>
        public List<ParentNamespace> Namespaces { get; set; } = new List<ParentNamespace>();
        /// <summary>
        /// The list of using declarations to be included
        /// </summary>
        public List<string> UsingNamespaces { get; set; } = new List<string>();
        /// <summary>
        /// The list of parameter names to be used in the constructor
        /// </summary>
        public List<string> Parameters { get; set; } = new List<string>();
        /// <summary>
        /// The list of parameter assignments
        /// </summary>
        public List<string> ParameterAssignments { get; set; } = new List<string>();
        /// <summary>
        /// The list of class members
        /// </summary>
        public List<string> Members { get; set; } = new List<string>();
        /// <summary>
        /// The list of parent classes that the class belongs in
        /// </summary>
        public List<(string className, string modifiers)> ParentClasses { get; set; } = new List<(string className, string modifiers)>();
        /// <summary>
        /// The modifier for the class
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// The list of base parameters
        /// </summary>
        public List<BaseParameter> BaseParameters { get; set; } = new List<BaseParameter>();
        /// <summary>
        /// Whether the constructor should be protected
        /// </summary>
        public bool GenerateProtectedConstructor { get; set; }
        /// <summary>
        /// The name of the file that is to be used to write the class definition to
        /// </summary>
        public string OutputName { get; set; }
        /// <summary>
        /// The list of parameter types
        /// </summary>
        public List<TypeDetails> ParameterTypes { get; set; } = new List<TypeDetails>();

        private string GetIndentation(int tabIndex) => string.Concat(Enumerable.Repeat("    ", tabIndex));

        /// <summary>
        /// Renders the class file
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            int nestedClasses = ParentClasses?.Count() ?? 0;
            int namespaceIndents = Namespaces.Count - 1;
            if (namespaceIndents < 0)
            {
                namespaceIndents = 0;
            }
            int classIndentCount = nestedClasses + namespaceIndents;
            int attributeIndentCount = nestedClasses + namespaceIndents + 2;
            int assignmentIndentCount = nestedClasses + namespaceIndents + 3;
            string nestedClassIndent = GetIndentation(namespaceIndents);
            string attributeIndent = GetIndentation(attributeIndentCount);
            string assignmentIndent = GetIndentation(assignmentIndentCount);
            string classIndent = GetIndentation(classIndentCount);

            string outerNamespaceList = string.Join(Environment.NewLine, UsingNamespaces.Select(p => p.Trim()));
            if (outerNamespaceList.Length > 0)
            {
                outerNamespaceList += Environment.NewLine;
                outerNamespaceList += Environment.NewLine;
            }

            string propertyList = string.Join(Environment.NewLine, Members.Select(p => $"{attributeIndent}{p}"));

            if (propertyList.Length > 0)
            {
                propertyList = $"{Environment.NewLine}{propertyList}{Environment.NewLine}";
            }

            string parameterList = $@"{string.Join(", ", Parameters)}";
            string assignments = $@"{string.Join(Environment.NewLine, ParameterAssignments.Select(p => $"{assignmentIndent}{p}"))}";
            string wrapStart = BuildWrapStart(nestedClassIndent);
            string wrapEnd = BuildWrapEnd(nestedClassIndent);

            string baseClass = string.Empty;
            string baseParameters = string.Empty;
            if (BaseParameters.Any())
            {
                string join = Parameters.Any() ? ", " : string.Empty;
                if (BaseParameters.Any(p => !p.AlreadyParameter))
                {
                    baseParameters = $@"{string.Join(", ", BaseParameters.Where(p => !p.AlreadyParameter).Select(p => $"{p.Type} {p.Name}"))}{join}";
                }
                baseClass = $" : base({string.Join(", ", BaseParameters.Select(p => p.Name))})";
            }

            string accessModifier = GenerateProtectedConstructor ? "protected" : "public";

            string built = $@"{outerNamespaceList}{BuildNamespaceStart()}{wrapStart}{classIndent}    {Modifier} class {ClassName}
{classIndent}    {{{propertyList}
{classIndent}        {accessModifier} {ClassName}({baseParameters}{parameterList}){baseClass}
{classIndent}        {{
{assignments}
{classIndent}        }}
{classIndent}    }}{wrapEnd}{BuildNamespaceEnd()}";

            return built;
        }

        private string BuildWrapStart(string tab)
        {
            string wrapStart = string.Empty;
            for (int x = 0; x < ParentClasses.Count; x++)
            {
                var indent = GetIndentation(x + 1);
                wrapStart += $@"{tab}{indent}{ParentClasses[x].modifiers} class {ParentClasses[x].className}
{tab}{indent}{{{Environment.NewLine}";
            }
            return wrapStart;
        }

        private string BuildWrapEnd(string tab)
        {
            string wrapEnd = string.Empty;
            for (int x = ParentClasses.Count; x > 0; x--)
            {
                wrapEnd += $"{tab}{GetIndentation(x)}}}";
                wrapEnd += Environment.NewLine;
            }

            if (wrapEnd.Length > 0)
            {
                wrapEnd = Environment.NewLine + wrapEnd;
            }

            return wrapEnd;
        }

        private string BuildNamespaceStart()
        {
            string namespaceStart = string.Empty;
            for (int x = 0; x < Namespaces.Count; x++)
            {
                string usings = BuildUsings(GetIndentation(x + 1), Namespaces[x].UsingDirectives);
                namespaceStart += $@"{GetIndentation(x)}namespace {Namespaces[x].NamespaceName}
{GetIndentation(x)}{{{usings}";
                namespaceStart += Environment.NewLine;
            }

            return namespaceStart;
        }

        private string BuildNamespaceEnd()
        {
            string namespaceEnd = string.Empty;
            if (!ParentClasses.Any())
            {
                namespaceEnd = Environment.NewLine;
            }
            for (int x = Namespaces.Count; x > 0; x--)
            {
                namespaceEnd += $"{GetIndentation(x - 1)}}}";
                if (x > 1)
                {
                    namespaceEnd += Environment.NewLine;
                }
            }

            return namespaceEnd;
        }

        private string BuildUsings(string tab, IEnumerable<string> names)
        {
            string usings = string.Join(Environment.NewLine, names.Select(p => $"{tab}{p.Trim()}"));
            if (usings.Length > 0)
            {
                usings += Environment.NewLine;
                usings = Environment.NewLine + usings;
            }
            return usings;
        }
    }
}
