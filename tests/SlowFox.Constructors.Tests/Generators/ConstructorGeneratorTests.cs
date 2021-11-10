using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Generators;
using SlowFox.Constructors.Tests.Base;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlowFox.Constructors.Tests.Generators
{
    public class ConstructorGeneratorTests : BaseWithAttributeTest<ConstructorGenerator>
    {
        [Fact]
        public async Task Class_WithAttribute_NoTypes_NothingCreated()
        {
            var classFile =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies]
    public partial class UserReader { }
}";

            await AssertNoGeneration(classFile);
        }

        [Fact]
        public async Task Class_WithAttribute_WithSingleType_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}
";

            var classFile2 =
@"using SlowFox;
using Logic.IO;

namespace Logic.IO
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
        public async Task ClassInSlowFoxNamespace_WithAttribute_WithSingleType_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox.Constructors.SampleClient.IO;
using System;

namespace SlowFox.Constructors.SampleClient
{
    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IReader))]
    public partial class Class1
    {
    }
}
";

            var classFile2 =
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowFox.Constructors.SampleClient
{
    public interface IDatabase
    {
    }
}
namespace SlowFox.Constructors.SampleClient.IO
{
    public interface IReader
    {
    }
}";

            var generated =
@"using SlowFox.Constructors.SampleClient.IO;
using System;

namespace SlowFox.Constructors.SampleClient
{
    public partial class Class1
    {
        private readonly IDatabase _database;
        private readonly IReader _reader;

        public Class1(IDatabase database, IReader reader)
        {
            _database = database;
            _reader = reader;
        }
    }
}";
            await AssertFullGeneration(generated, "SlowFox.Constructors.SampleClient.Class1.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttribute_WithMultipleTypes_GenerateMembersAndConstructor()
        {
            var classFile1 =
@"using SlowFox;
using Logic.IO;
using Logic.Exports;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase), typeof(IDataSender))]
    public partial class UserReader { }
}
";

            var classFile2 =
@"namespace Logic.IO
{
    public interface IDatabase { }
}

namespace Logic.Exports
{
    public interface IDataSender { }
}
";

            var generated =
@"using SlowFox;
using Logic.IO;
using Logic.Exports;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;
        private readonly IDataSender _dataSender;

        public UserReader(IDatabase database, IDataSender dataSender)
        {
            _database = database;
            _dataSender = dataSender;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttribute_WithDuplicateType_GenerateMemberAndConstructorsWithUniqueNames()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase), typeof(IDatabase))]
    public partial class UserReader { }
}

namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;
        private readonly IDatabase _database2;

        public UserReader(IDatabase database, IDatabase database2)
        {
            _database = database;
            _database2 = database2;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1);
        }

        [Fact]
        public async Task Class_WithAttribute_WithTypeWithInlineNamespace_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(Logic.IO.IDatabase))]
    public partial class UserReader { }
}";
            var classFile2 = @"
namespace Logic.IO
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly Logic.IO.IDatabase _database;

        public UserReader(Logic.IO.IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttribute_WithTypeWithInlinePartialNamespace_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;
using Logic;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IO.IDatabase))]
    public partial class UserReader { }
}";
            var classFile2 = @"
namespace Logic.IO
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;
using Logic;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IO.IDatabase _database;

        public UserReader(IO.IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttribute_WithTypeWithAliasNamespace_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;
using ExIO = ExternalProject.Helpers.IO;

namespace Logic.Readers
{
    [InjectDependencies(typeof(ExIO.IDatabase))]
    public partial class UserReader { }
}";
            var classFile2 = @"
namespace ExternalProject.Helpers.IO
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;
using ExIO = ExternalProject.Helpers.IO;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly ExIO.IDatabase _database;

        public UserReader(ExIO.IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttribute_WithTypeWithAliasPartialNamespace_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;
using Helper = ExternalProject.Helpers;

namespace Logic.Readers
{
    [InjectDependencies(typeof(Helper.IO.IDatabase))]
    public partial class UserReader { }
}";
            var classFile2 = @"
namespace ExternalProject.Helpers.IO
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;
using Helper = ExternalProject.Helpers;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly Helper.IO.IDatabase _database;

        public UserReader(Helper.IO.IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithAttributeViaUsing_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}";

            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated =
@"using SlowFox;

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
        public async Task Class_WithAttributeViaNamespace_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"namespace Logic.Readers
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}";

            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated =
@"namespace Logic.Readers
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
        public async Task Class_WithAttributeViaAlias_GenerateMemberAndConstructor()
        {
            var classFile1 =
@"using S = SlowFox.InjectDependenciesAttribute;

namespace Logic.Readers
{
    [S(typeof(IDatabase))]
    public partial class UserReader { }
}";

            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated =
@"using S = SlowFox.InjectDependenciesAttribute;

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
        public async Task TwoDifferentClasses_GenerateUniqueFilesForBoth()
        {
            var classFile1 =
@"using Logic.Readers;

namespace Logic.Readers.Section1
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader1 { }
}

namespace Logic.Readers.Section2
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader2 { }
}";

            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated1 =
@"using Logic.Readers;

namespace Logic.Readers.Section1
{
    public partial class UserReader1
    {
        private readonly IDatabase _database;

        public UserReader1(IDatabase database)
        {
            _database = database;
        }
    }
}";

            var generated2 =
@"using Logic.Readers;

namespace Logic.Readers.Section2
{
    public partial class UserReader2
    {
        private readonly IDatabase _database;

        public UserReader2(IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertGenerationTwoOutputs(generated1, "Logic.Readers.Section1.UserReader1.Generated.cs", generated2, "Logic.Readers.Section2.UserReader2.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task TwoDifferentClasses_SameNameDifferentNamespace_GenerateUniqueFilesForBoth()
        {
            var classFile1 =
@"using Logic.Readers;

namespace Logic.Readers.Section1
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader1 { }
}

namespace Logic.Readers.Section2
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader1 { }
}";

            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

            var generated1 =
@"using Logic.Readers;

namespace Logic.Readers.Section1
{
    public partial class UserReader1
    {
        private readonly IDatabase _database;

        public UserReader1(IDatabase database)
        {
            _database = database;
        }
    }
}";

            var generated2 =
@"using Logic.Readers;

namespace Logic.Readers.Section2
{
    public partial class UserReader1
    {
        private readonly IDatabase _database;

        public UserReader1(IDatabase database)
        {
            _database = database;
        }
    }
}";
            await AssertGenerationTwoOutputs(generated1, "Logic.Readers.Section1.UserReader1.Generated.cs", generated2, "Logic.Readers.Section2.UserReader1.Generated.cs", classFile1, classFile2);
        }

        [Fact]
        public async Task Class_WithNonNullAttribute_IgnoreNullCheck()
        {
            var classFile1 =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    [InjectDependencies(typeof(DateTime), typeof(DateTime?), typeof(IDatabase))]
    public partial class UserReader { }
}
";
            var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.include_nullcheck = true
";
            var generated =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly DateTime _dateTime;
        private readonly DateTime? _dateTime2;
        private readonly IDatabase _database;

        public UserReader(DateTime dateTime, DateTime? dateTime2, IDatabase database)
        {
            _dateTime = dateTime;
            _dateTime2 = dateTime2;
            _database = database ?? throw new System.ArgumentNullException(nameof(database));
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1, classFile2 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithNonNullIntAttribute_IgnoreNullCheck()
        {
            var classFile1 =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    [InjectDependencies(typeof(int))]
    public partial class UserReader { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.include_nullcheck = true
";
            var generated =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly int _int;

        public UserReader(int @int)
        {
            _int = @int;
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithNullIntAttribute_IgnoreNullCheck()
        {
            var classFile1 =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    [InjectDependencies(typeof(int?))]
    public partial class UserReader { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.include_nullcheck = true
";
            var generated =
@"using SlowFox;
using System;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly int? _int;

        public UserReader(int? @int)
        {
            _int = @int;
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithAttribute_ConfigureWithoutUnderscores_GenerateWithoutUnderscore()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
    public interface IDatabase { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.skip_underscores = true
";
            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase database;

        public UserReader(IDatabase database)
        {
            this.database = database;
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithAttribute_ConfigureWithUnderscores_GenerateWithUnderscore()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }

    public interface IDatabase { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.skip_underscores = false
";
            var generated =
@"using SlowFox;

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
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithAttribute_ConfigureWithoutNullCheck_GenerateWithoutNullCheck()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
    public interface IDatabase { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.include_nullcheck = false
";
            var generated =
@"using SlowFox;

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
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_WithAttribute_ConfigureWithNullCheck_GenerateWithNullCheck()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
    public interface IDatabase { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.include_nullcheck = true
";
            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly IDatabase _database;

        public UserReader(IDatabase database)
        {
            _database = database ?? throw new System.ArgumentNullException(nameof(database));
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_IntDepdendency_SkipUnderscores_GenerateCorrectMemberName()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(int))]
    public partial class UserReader { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.skip_underscores = true
";
            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly int @int;

        public UserReader(int @int)
        {
            this.@int = @int;
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task Class_IntDepdendency_IncludeUnderscores_GenerateCorrectMemberName()
        {
            var classFile1 =
@"using SlowFox;

namespace Logic.Readers
{
    [InjectDependencies(typeof(int))]
    public partial class UserReader { }
}
";
            var config =
@"
is_global = true
slowfox_generation.constructors.skip_underscores = false
";
            var generated =
@"using SlowFox;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly int _int;

        public UserReader(int @int)
        {
            _int = @int;
        }
    }
}";
            await new Verifiers.CSharpMultipleSourceGeneratorVerifier<ConstructorGenerator, InjectDependenciesAttributeGenerator>.Test
            {
                TestState =
                {
                    Sources = { classFile1 },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
                        (typeof(InjectDependenciesAttributeGenerator), ExpectedAttributeFileName, SourceText.From(ExpectedAttributeContents, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
            }.RunAsync();
        }
    }
}
