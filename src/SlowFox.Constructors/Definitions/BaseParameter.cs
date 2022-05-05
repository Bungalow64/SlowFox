using Microsoft.CodeAnalysis;

namespace SlowFox.Constructors.Definitions
{
    /// <summary>
    /// Defines a parameter used in a base class
    /// </summary>
    internal class BaseParameter
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the parameter
        /// </summary>
        public ITypeSymbol Type { get; set; }
        /// <summary>
        /// Whether the type already exists as a dependency in the parent class
        /// </summary>
        public bool AlreadyParameter { get; set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="alreadyParameter"></param>
        public BaseParameter(string name, ITypeSymbol type, bool alreadyParameter)
        {
            Name = name;
            Type = type;
            AlreadyParameter = alreadyParameter;
        }
    }
}