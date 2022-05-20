using System;

namespace SlowFox
{
    /// <summary>
    /// The attribute used to exclude specific types from being created as mock objects
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeMocksAttribute : Attribute
    {
        /// <summary>
        /// The list of types to be excluded
        /// </summary>
        public Type[] Types { get; set; }
        /// <summary>
        /// The default constructor for the attribute, defining no types to be excluded
        /// </summary>
        public ExcludeMocksAttribute() { }
        /// <summary>
        /// The constructor for the attribute, defining one or more types to be excluded
        /// </summary>
        /// <param name="types">The array of types to be excluded</param>
        /// <remarks>All excluded types will be created as parameters on the generated Create method</remarks>
        public ExcludeMocksAttribute(params Type[] types) => Types = types;
    }
}