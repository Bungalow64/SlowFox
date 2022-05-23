using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SlowFox.Core.Logic;
using System.Collections.Generic;

namespace SlowFox.Core.Definitions
{
    /// <summary>
    ///  The definition of a type
    /// </summary>
    public class TypeDetails
    {
        /// <summary>
        /// The original name of the type
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// The short name of the type
        /// </summary>
        public string ShortTypeName { get; set; }
        /// <summary>
        /// The generated name of the type
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Whether the type is nullable
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// Whether the type requires a prefix
        /// </summary>
        public bool RequiresPrefix => TypeName.Replace("?", "") == Name;
        /// <summary>
        /// The parameter name of the type
        /// </summary>
        public string InputName => RequiresPrefix ? $"@{Name}" : Name;
        /// <summary>
        /// The prefix of the field
        /// </summary>
        public string FieldPrefix { get; set; }
        /// <summary>
        /// The type object itself
        /// </summary>
        public ITypeSymbol Type { get; set; }
        /// <summary>
        /// The member name of the type
        /// </summary>
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

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="semanticModel"></param>
        /// <param name="typeSyntax"></param>
        /// <param name="existingNames"></param>
        /// <param name="fieldPrefix"></param>
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

                TypeName = ShortTypeName;
            }
        }
    }
}