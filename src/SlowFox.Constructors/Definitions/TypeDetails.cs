using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Constructors.Logic;
using System.Collections.Generic;

namespace SlowFox.Constructors.Definitions
{
    internal class TypeDetails
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
            }
            if (semanticModel != null)
            {
                var type = semanticModel.GetTypeInfo(typeSyntax).Type;

                if (type != null)
                {
                    IsNullable = type.IsReferenceType;
                }
            }
        }
    }
}