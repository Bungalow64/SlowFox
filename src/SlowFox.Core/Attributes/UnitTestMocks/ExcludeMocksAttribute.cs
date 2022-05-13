using System;

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeMocksAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public ExcludeMocksAttribute() { }
        public ExcludeMocksAttribute(params Type[] types) => Types = types;
    }
}