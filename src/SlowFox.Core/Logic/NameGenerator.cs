using System;
using System.Collections.Generic;
using System.Linq;

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

            if (typeName.StartsWith("I", StringComparison.Ordinal))
            {
                typeName = $"{typeName.Substring(1, typeName.Length - 1)}";
            }

            if (typeName.Length > 1)
            {
                if (char.IsUpper(typeName[0]))
                {
                    typeName = $"{char.ToLower(typeName[0])}{typeName.Substring(1, typeName.Length - 1)}";
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
    }
}
