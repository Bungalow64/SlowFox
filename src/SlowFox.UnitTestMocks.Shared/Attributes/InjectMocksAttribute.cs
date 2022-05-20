using System;

namespace SlowFox
{
    /// <summary>
    /// The attribute used to specify the type for which the mocks will be generated
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectMocksAttribute : Attribute
    {
        /// <summary>
        /// The type for which the mocks will be generated
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The default constructor for the attribute, defining no type
        /// </summary>
        public InjectMocksAttribute() { }
        /// <summary>
        /// The default constructor for the attribute, defining the type for which the mocks will be generated
        /// </summary>
        public InjectMocksAttribute(Type type) => Type = type;
    }
}