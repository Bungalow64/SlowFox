using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowFox.Core.Logic
{
    /// <summary>
    /// A static helper class to generate property and parameter names
    /// </summary>
    public static class NameGenerator
    {
        /// <summary>
        /// Gets the name based on the type provided
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <param name="usedNames">The list of names already used, to avoid conflicts</param>
        /// <returns>Returns the generated name</returns>
        public static string GetName(string typeName, List<string> usedNames)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return string.Empty;
            }

            typeName = typeName.Trim();

            if (typeName.All(char.IsUpper))
            {
                typeName = typeName.ToLower();
            }

            if (typeName.StartsWith("(") && typeName.EndsWith(")"))
            {
                List<string> parts = SplitGenerics(typeName)
                    .Select(GetName).ToList();

                for (int x = 1; x < parts.Count; x++)
                {
                    if (!string.IsNullOrWhiteSpace(parts[x]))
                    {
                        parts[x] = ToFirstUpper(parts[x]);
                    }
                }

                typeName = string.Join(string.Empty, parts);
            }

            if (typeName.EndsWith(">") && typeName.Contains("<"))
            {
                string genericType = GetName(typeName.Substring(0, typeName.IndexOf("<")));

                var parts = SplitGenerics(typeName.Substring(typeName.IndexOf("<") + 1, typeName.LastIndexOf(">") - typeName.IndexOf("<") - 1))
                    .Select(GetName).ToList();

                for (int x = 1; x < parts.Count; x++)
                {
                    if (!string.IsNullOrWhiteSpace(parts[x]))
                    {
                        parts[x] = ToFirstUpper(parts[x]);
                    }
                }

                typeName = $"{string.Join(string.Empty, parts)}{ToFirstUpper(genericType)}";
            }

            if (typeName.Contains("."))
            {
                typeName = typeName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
            }

            if (typeName == "I")
            {
                typeName = "i";
            }

            if (typeName.StartsWith("@", StringComparison.OrdinalIgnoreCase))
            {
                typeName = typeName.Trim('@');
            }

            if (typeName.Length > 1)
            {
                if (typeName.StartsWith("I", StringComparison.Ordinal) && char.IsUpper(typeName[1]))
                {
                    typeName = $"{typeName.Substring(1, typeName.Length - 1)}";
                }

                if (char.IsUpper(typeName[0]))
                {
                    typeName = ToFirstLower(typeName);
                }
            }

            typeName = typeName.Replace("?", "");

            string originalName = typeName;

            int index = 2;
            while (usedNames.Contains(typeName))
            {
                typeName = $"{originalName}{index++}";
            }

            usedNames.Add(typeName);

            return $"{typeName}";
        }
        private static string GetName(string typeName) => GetName(typeName, new List<string>());

        private static string ToFirstUpper(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || char.IsUpper(value[0]))
            {
                return value;
            }
            return $"{char.ToUpper(value[0])}{value.Substring(1, value.Length - 1)}";
        }

        private static string ToFirstLower(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || char.IsLower(value[0]))
            {
                return value;
            }
            return $"{char.ToLower(value[0])}{value.Substring(1, value.Length - 1)}";
        }

        // e.g., User, ICollection<(Group, Address)>, Repository<Branch>, Outer<Inner<SubInner>>, Branch
        private static IList<string> SplitGenerics(string value)
        {
            var finalParts = new List<string>();
            int depth = 0;
            bool isSkipping = false;

            var currentPart = new StringBuilder();

            char[] characters = value.ToCharArray();

            foreach (char character in characters)
            {
                if (depth == 0)
                {
                    if (character == '(' || character == ')')
                    {
                        isSkipping = false;
                        continue;
                    }

                    if (character == ' ')
                    {
                        if (currentPart.Length > 0)
                        {
                            isSkipping = true;
                        }
                        continue;
                    }

                    if (character == ',')
                    {
                        isSkipping = false;
                        finalParts.Add(currentPart.ToString());
                        currentPart.Clear();
                        continue;
                    }

                    if (isSkipping)
                    {
                        continue;
                    }
                }

                if (character == '<')
                {
                    depth++;
                }

                if (character == '>')
                {
                    depth--;
                }

                currentPart.Append(character);
            }

            finalParts.Add(currentPart.ToString());

            return finalParts;
        }
    }
}
