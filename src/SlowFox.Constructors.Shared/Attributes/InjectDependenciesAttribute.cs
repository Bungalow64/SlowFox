using System;

namespace SlowFox
{
    /// <summary>
    /// The attribute used to inject dependencies into a class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        /// <summary>
        /// The list of types to be injected
        /// </summary>
        public Type[] Types { get; set; }
        /// <summary>
        /// The default constructor for the attribute, defining no types to be injected
        /// </summary>
        public InjectDependenciesAttribute() { }
        /// <summary>
        /// The constructor for the attribute, defining one or more types to be injected
        /// </summary>
        /// <param name="types">The array of types to be injected</param>
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}