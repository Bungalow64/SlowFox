using Microsoft.CodeAnalysis;
using SlowFox.Tests.Shared.Base;
using System.Collections.Generic;

namespace SlowFox.Constructors.Tests.Base
{
    public abstract class BaseConstructorTest<TGenerator1> : BaseWithAttributeTest<TGenerator1>
        where TGenerator1 : ISourceGenerator, new()
    {
        protected override string ExpectedAttributeContents => @"using System;

namespace SlowFox
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectDependenciesAttribute : Attribute
    {
        public Type[] Types { get; set; }
        public InjectDependenciesAttribute() { }
        public InjectDependenciesAttribute(params Type[] types) => Types = types;
    }
}";

        protected override IList<string> MetadataReferences => new List<string>();
    }
}
