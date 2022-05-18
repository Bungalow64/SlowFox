using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Logic;
using System.Collections.Generic;

namespace SlowFox.Core.Definitions
{
    public class TypeDetails
    {
        public string TypeName { get; set; }
        public string ShortTypeName { get; set; }
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public bool RequiresPrefix => TypeName.Replace("?", "") == Name;
        public string InputName => RequiresPrefix ? $"@{Name}" : Name;
        public string FieldPrefix { get; set; }
        public ITypeSymbol Type { get; set; }
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
                    ShortTypeName = identifierNameSyntax.Identifier.Text;
                    break;
                default:
                    ShortTypeName = typeSyntax.GetText().ToString();
                    break;
            }

            Name = NameGenerator.GetName(ShortTypeName, existingNames);

            if (semanticModel != null)
            {
                Type = semanticModel.GetTypeInfo(typeSyntax).Type;

                if (Type != null)
                {
                    IsNullable = Type.IsReferenceType;
                }

                //TypeName = type?.ToString() ?? ShortTypeName;
                TypeName = ShortTypeName;
            }
        }
    }
}