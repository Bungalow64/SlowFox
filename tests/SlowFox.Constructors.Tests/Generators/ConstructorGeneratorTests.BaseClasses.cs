using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Generators;
using SlowFox.Constructors.Tests.Base;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlowFox.Constructors.Tests.Generators;

public partial class ConstructorGeneratorTests
{
    [Fact]
    public async Task BaseClass_WithSingleParameterConstructor_IncludeParameterInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }

public abstract class BaseReader
{
    private IConfiguration _configuration;

    protected BaseReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(Logic.IO.IConfiguration configuration, IDatabase database) : base(configuration)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithSingleParameterConstructorAndStatic_IncludeParameterInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }

public class BaseReader
{
    private IConfiguration _configuration;

    static BaseReader() { }

    public BaseReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(Logic.IO.IConfiguration configuration, IDatabase database) : base(configuration)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithMultipleParameterConstructor_IncludeParametersInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }

public abstract class BaseReader
{
    private IConfigurationA _configurationA;
    private IConfigurationB _configurationB;

    protected BaseReader(IConfigurationA configurationA, IConfigurationB configurationB)
    {
        _configurationA = configurationA;
        _configurationB = configurationB;
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfigurationA { }
    public interface IConfigurationB { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(Logic.IO.IConfigurationA configurationA, Logic.IO.IConfigurationB configurationB, IDatabase database) : base(configurationA, configurationB)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithNoParameterConstructor_DoNotCallBaseConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }

public abstract class BaseReader
{
    protected BaseReader()
    {
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(IDatabase database)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_AndInterface_WithNoParameterConstructor_DoNotCallBaseConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader, IUserReader { }

public abstract class BaseReader
{
    protected BaseReader()
    {
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IUserReader { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(IDatabase database)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithSingleParameterConstructor_ExplicitlyInjectSameType_UseSameTypeInConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase), typeof(IConfiguration))]
public partial class UserReader : BaseReader { }

public abstract class BaseReader
{
    private IConfiguration _configuration;

    protected BaseReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
}
";

        var generated =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;

        public UserReader(IDatabase database, IConfiguration configuration) : base(configuration)
        {
            _database = database;
            _configuration = configuration;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }
}

