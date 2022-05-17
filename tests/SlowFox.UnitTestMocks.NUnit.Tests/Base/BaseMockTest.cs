using Microsoft.CodeAnalysis;
using SlowFox.Tests.Core.Base;
using System.Collections.Generic;

namespace SlowFox.UnitTestMocks.NUnit.Tests.Base
{
    public abstract class BaseMockTest<TGenerator1> : BaseWithAttributeTest<TGenerator1>
        where TGenerator1 : ISourceGenerator, new()
    {
        protected override string ExpectedAttributeContents => @"using System;

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectMocksAttribute : Attribute
    {
        public Type Type { get; set; }
        public InjectMocksAttribute() { }
        public InjectMocksAttribute(Type type) => Type = type;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeMocksAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public ExcludeMocksAttribute() { }
        public ExcludeMocksAttribute(params Type[] types) => Types = types;
    }
}";

        protected override IList<string> MetadataReferences => new List<string>
        {
            "Moq.dll",
            "nunit.framework.dll"
        };
    }
}
