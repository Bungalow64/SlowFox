namespace ExternalProject.Net7.UnitTestMocks.Sample.DependencyTypeTests;

public class LocalClassDependency
{
    public class LocalClass
    {
        public virtual string Name { get; set; } = string.Empty;
    }

    private readonly LocalClass _localValue;

    public LocalClassDependency(LocalClass localValue)
    {
        _localValue = localValue;
    }

    public LocalClass GetLocalClass() => _localValue;
}
