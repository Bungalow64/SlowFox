using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowFox.Constructors.Logic
{
    internal static class NameGenerator
    {
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
