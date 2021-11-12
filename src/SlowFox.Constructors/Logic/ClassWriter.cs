using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Constructors.Logic
{
    public class ClassWriter
    {
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public int TabIndex { get; set; }
        public List<string> UsingNamespaces { get; set; }
        public List<string> Parameters { get; set; }
        public List<string> ParameterAssignments { get; set; }
        public List<string> Members { get; set; }
        public string Indentation => string.Concat(Enumerable.Repeat("    ", TabIndex));
        public bool IsNested => ParentClasses?.Any() ?? false;
        public List<(string className, string modifiers)> ParentClasses { get; set; }
        public string Modifier { get; set; }

        private string GetIndentation(int tabIndex) => string.Concat(Enumerable.Repeat("    ", tabIndex));

        public string Render()
        {
            string indentation = Indentation;
            string indentation2 = GetIndentation(IsNested ? 1 : 2);
            string indentation3 = GetIndentation(IsNested ? 2 : 3);
            string nested = GetIndentation(ParentClasses?.Count() ?? 0);

            string namespaceList = string.Join("", UsingNamespaces);
            if (namespaceList.Length > 0)
            {
                namespaceList += Environment.NewLine;
            }

            string propertyList = string.Join(Environment.NewLine, Members.Select(p => $"{indentation}{nested}{indentation2}{p}"));

            if (propertyList.Length > 0)
            {
                propertyList += Environment.NewLine;
            }

            string parameterList = $@"{string.Join(", ", Parameters)}";
            string assignments = $@"{string.Join(Environment.NewLine, ParameterAssignments.Select(p => $"{indentation}{nested}{indentation3}{p}"))}";

            if (IsNested)
            {
                string inner = $@"{indentation}{nested}{Modifier} class {ClassName}
{indentation}{nested}    {{
{indentation}{nested}{propertyList}
{indentation}{nested}        public {ClassName}({parameterList})
{indentation}{nested}        {{
{indentation}{nested}{assignments}
{indentation}{nested}        }}
{indentation}{nested}    }}";

                string wrapStart = string.Empty;
                for (int x = 0; x < ParentClasses.Count; x++)
                {
                    var indent = GetIndentation(x + 1);
                    wrapStart += $@"{indent}{ParentClasses[x].modifiers} class {ParentClasses[x].className}
{indent}{{";
                }

                string wrapEnd = string.Empty;
                for (int x = 0; x < ParentClasses.Count; x++)
                {
                    wrapEnd += $"{GetIndentation(x + 1)}}}";
                }



                return $@"{indentation}{namespaceList}namespace {Namespace}
{indentation}{{
{indentation}{wrapStart}
{indentation}    {inner}
{indentation}{wrapEnd}
{indentation}}}";
            }

            return $@"{indentation}{namespaceList}namespace {Namespace}
{indentation}{{
{indentation}    {Modifier} class {ClassName}
{indentation}    {{
{indentation}{propertyList}
{indentation}        public {ClassName}({parameterList})
{indentation}        {{
{indentation}{assignments}
{indentation}        }}
{indentation}    }}
{indentation}}}";
        }
    }
}
