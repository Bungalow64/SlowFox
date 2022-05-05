using System.Collections.Generic;
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

    [Fact]
    public async Task BaseClass_WithInjectedDependency_IncludeParameterInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }

[InjectDependencies(typeof(IConfiguration))]
public abstract partial class BaseReader { }
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
}
";

        var generated1 =
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

        var generated2 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReader
    {
        private readonly IConfiguration _configuration;

        protected BaseReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}";
        await AssertGenerationTwoOutputs(generated1, "Logic.Readers.UserReader.Generated.cs", generated2, "Logic.Readers.BaseReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithInjectedDependency_BaseClassFirst_IncludeParameterInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IConfiguration))]
public abstract partial class BaseReader { }

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader { }
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
}
";

        var generated1 =
        @"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReader
    {
        private readonly IConfiguration _configuration;

        protected BaseReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}";

        var generated2 =
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
        await AssertGenerationTwoOutputs(generated1, "Logic.Readers.BaseReader.Generated.cs", generated2, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithEmptyAttribute_GenerateForBaseOnly()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies]
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
        public UserReader(Logic.IO.IConfiguration configuration) : base(configuration)
        {

        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_WithInjectedDependencyAndInterface_IncludeParameterInGeneratedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReader, IUserReader1, IUserReader2 { }

[InjectDependencies(typeof(IConfiguration))]
public abstract partial class BaseReader : IBaseReader { }
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
    public interface IBaseReader { }
    public interface IUserReader1 { }
    public interface IUserReader2 { }
}
";

        var generated1 =
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

        var generated2 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReader
    {
        private readonly IConfiguration _configuration;

        protected BaseReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}";
        await AssertGenerationTwoOutputs(generated1, "Logic.Readers.UserReader.Generated.cs", generated2, "Logic.Readers.BaseReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_MultipleWithInjectedDependency_IncludeParametersAndConstructorsAtEachLevel()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReaderA { }

[InjectDependencies(typeof(IConfiguration))]
public abstract partial class BaseReaderA : BaseReaderB { }

[InjectDependencies(typeof(IUserReader))]
public abstract partial class BaseReaderB : BaseReaderC { }

[InjectDependencies(typeof(IUserWriter))]
public abstract partial class BaseReaderC { }
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
    public interface IUserReader { }
    public interface IUserWriter { }
}
";

        var generatedUserReader =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(Logic.IO.IUserWriter userWriter, Logic.IO.IUserReader userReader, Logic.IO.IConfiguration configuration, IDatabase database) : base(userWriter, userReader, configuration)
        {
            _database = database;
        }
    }
}";

        var generatedBaseReaderA =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReaderA
    {
        private readonly IConfiguration _configuration;

        protected BaseReaderA(Logic.IO.IUserWriter userWriter, Logic.IO.IUserReader userReader, IConfiguration configuration) : base(userWriter, userReader)
        {
            _configuration = configuration;
        }
    }
}";

        var generatedBaseReaderB =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReaderB
    {
        private readonly IUserReader _userReader;

        protected BaseReaderB(Logic.IO.IUserWriter userWriter, IUserReader userReader) : base(userWriter)
        {
            _userReader = userReader;
        }
    }
}";

        var generatedBaseReaderC =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReaderC
    {
        private readonly IUserWriter _userWriter;

        protected BaseReaderC(IUserWriter userWriter)
        {
            _userWriter = userWriter;
        }
    }
}";
        await AssertMultipleGenerations(new Dictionary<string, string>
        {
            { "Logic.Readers.UserReader.Generated.cs", generatedUserReader },
            { "Logic.Readers.BaseReaderA.Generated.cs", generatedBaseReaderA },
            { "Logic.Readers.BaseReaderB.Generated.cs", generatedBaseReaderB },
            { "Logic.Readers.BaseReaderC.Generated.cs", generatedBaseReaderC }
        }, classFile1, classFile2);
    }

    [Fact]
    public async Task BaseClass_MultipleWithInjectedDependency_OnlySomeHaveDependencies_IncludeParametersAndConstructorsAtEachLevel()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader : BaseReaderA { }

[InjectDependencies]
public abstract partial class BaseReaderA : BaseReaderB { }

[InjectDependencies(typeof(IUserReader))]
public abstract partial class BaseReaderB : BaseReaderC { }

public abstract partial class BaseReaderC { }
";

        var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
    public interface IConfiguration { }
    public interface IUserReader { }
    public interface IUserWriter { }
}
";

        var generatedUserReader =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(Logic.IO.IUserReader userReader, IDatabase database) : base(userReader)
        {
            _database = database;
        }
    }
}";

        var generatedBaseReaderA =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReaderA
    {
        protected BaseReaderA(Logic.IO.IUserReader userReader) : base(userReader)
        {

        }
    }
}";

        var generatedBaseReaderB =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    public abstract partial class BaseReaderB
    {
        private readonly IUserReader _userReader;

        protected BaseReaderB(IUserReader userReader)
        {
            _userReader = userReader;
        }
    }
}";

        await AssertMultipleGenerations(new Dictionary<string, string>
        {
            { "Logic.Readers.UserReader.Generated.cs", generatedUserReader },
            { "Logic.Readers.BaseReaderA.Generated.cs", generatedBaseReaderA },
            { "Logic.Readers.BaseReaderB.Generated.cs", generatedBaseReaderB }
        }, classFile1, classFile2);
    }
}

