using Microsoft.CodeAnalysis.Text;
using SlowFox.Constructors.Generators;
using SlowFox.Constructors.Tests.Base;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlowFox.Constructors.Tests.Generators;

public partial class ConstructorGeneratorTests : BaseConstructorTest<ConstructorGenerator>
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, classFile2, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
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
        await new Verifiers.CSharpSourceGeneratorVerifier<ConstructorGenerator>.Test
        {
            TestState =
                {
                    Sources = { classFile1, ExpectedAttributeContents },
                    AnalyzerConfigFiles = { ("/.editorconfig", config) },
                    GeneratedSources =
                    {
                        (typeof(ConstructorGenerator), "Logic.Readers.UserReader.Generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    }
                }
        }.RunAsync();
    }

    [Fact]
    public async Task NestedClass_GenerateCodeForNested()
    {
        var classFile1 =
@"namespace Logic.Readers
{
    public partial class UserReader 
    {
        [SlowFox.InjectDependencies(typeof(IDatabase))]
        private partial class InnerClass { }
    }
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
        private partial class InnerClass
        {
            private readonly IDatabase _database;

            public InnerClass(IDatabase database)
            {
                _database = database;
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader-InnerClass.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task NestedClassMultiple_GenerateCodeForNested()
    {
        var classFile1 =
@"namespace Logic.Readers
{
    public partial class UserReader
    {
        private partial class UpperClass
        {
            private partial class MiddleClass
            {
                [SlowFox.InjectDependencies(typeof(IDatabase))]
                private partial class InnerClass
                {
                }
            }
        }
    }
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
        private partial class UpperClass
        {
            private partial class MiddleClass
            {
                private partial class InnerClass
                {
                    private readonly IDatabase _database;

                    public InnerClass(IDatabase database)
                    {
                        _database = database;
                    }
                }
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader-UpperClass-MiddleClass-InnerClass.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task InternalClass_GenerateInternalCode()
    {
        var classFile1 =
@"namespace Logic.Readers
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    internal partial class UserReader { }
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
    internal partial class UserReader
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
    public async Task UsingDirectivesOutsideClass_GenerateDirectivesOutsideClass()
    {
        var classFile1 =
@"using Logic.Readers.IO;

namespace Logic.Readers
{
    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}";

        var classFile2 = @"
namespace Logic.Readers.IO
{
    public interface IDatabase { }
}";

        var generated =
@"using Logic.Readers.IO;

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
    public async Task UsingDirectivesInsideClass_GenerateDirectivesInsideClass()
    {
        var classFile1 =
@"namespace Logic.Readers
{
    using Logic.Readers.IO;

    [SlowFox.InjectDependencies(typeof(IDatabase))]
    public partial class UserReader { }
}";

        var classFile2 = @"
namespace Logic.Readers.IO
{
    public interface IDatabase { }
}";

        var generated =
@"namespace Logic.Readers
{
    using Logic.Readers.IO;

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
    public async Task UsingDirectivesInsideAndOutsideClass_GenerateDirectivesInCorrectPlace()
    {
        var classFile1 =
@"using Logic.Readers.Data;

namespace Logic.Readers
{
    using Logic.Readers.IO;

    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IFileReader))]
    public partial class UserReader { }
}";

        var classFile2 = @"
namespace Logic.Readers.IO
{
    public interface IDatabase { }
}

namespace Logic.Readers.Data
{
    public interface IFileReader { }
}";

        var generated =
@"using Logic.Readers.Data;

namespace Logic.Readers
{
    using Logic.Readers.IO;

    public partial class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;

        public UserReader(IDatabase database, IFileReader fileReader)
        {
            _database = database;
            _fileReader = fileReader;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task UsingDirectivesMultipleInsideAndOutsideClass_GenerateDirectivesInCorrectPlace()
    {
        var classFile1 =
@"using Logic.Readers.Data;
using System;

namespace Logic.Readers
{
    using Logic.Readers.IO;
    using System.Text;

    [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IFileReader))]
    public partial class UserReader { }
}";

        var classFile2 = @"
namespace Logic.Readers.IO
{
    public interface IDatabase { }
}

namespace Logic.Readers.Data
{
    public interface IFileReader { }
}";

        var generated =
@"using Logic.Readers.Data;
using System;

namespace Logic.Readers
{
    using Logic.Readers.IO;
    using System.Text;

    public partial class UserReader
    {
        private readonly IDatabase _database;
        private readonly IFileReader _fileReader;

        public UserReader(IDatabase database, IFileReader fileReader)
        {
            _database = database;
            _fileReader = fileReader;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task NestedNamespace_GenerateFileWithCorrectNamespace()
    {
        var classFile1 =
@"namespace Logic
{
    namespace Readers
    {
        [SlowFox.InjectDependencies(typeof(IDatabase))]
        public partial class UserReader { }
    }
}";

        var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}";

        var generated =
@"namespace Logic
{
    namespace Readers
    {
        public partial class UserReader
        {
            private readonly IDatabase _database;

            public UserReader(IDatabase database)
            {
                _database = database;
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task NestedNamespaceWithNestedUsings_GenerateUsingsAtCorrectLevels()
    {
        var classFile1 =
@"using System;

namespace Logic
{
    using ExternalHelpers.IO;

    namespace Readers
    {
        using System.Text;
        using ExternalHelpers.Communication;

        [SlowFox.InjectDependencies(typeof(IDatabase), typeof(IEmailer))]
        public partial class UserReader { }
    }
}";

        var classFile2 = @"
namespace ExternalHelpers.IO
{
    public interface IDatabase { }
}

namespace ExternalHelpers.Communication
{
    public interface IEmailer { }
}";

        var generated =
@"using System;

namespace Logic
{
    using ExternalHelpers.IO;

    namespace Readers
    {
        using System.Text;
        using ExternalHelpers.Communication;

        public partial class UserReader
        {
            private readonly IDatabase _database;
            private readonly IEmailer _emailer;

            public UserReader(IDatabase database, IEmailer emailer)
            {
                _database = database;
                _emailer = emailer;
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task NestedNamespaceWithMultipleNestedClass_GenerateCodeForNested()
    {

        var classFile1 =
@"using System;
using System.Text;

namespace Logic.Readers
{
    using Logic;
    using System.Threading.Tasks;

    namespace Sub1
    {
        using System.Buffers;
        using System.Data;

        namespace Sub2
        {
            using System.Configuration;
            using System.Dynamic;

            public partial class UserReader
            {
                private partial class UpperClass
                {
                    private partial class MiddleClass
                    {
                        [SlowFox.InjectDependencies(typeof(IDatabase))]
                        private partial class InnerClass
                        {
                        }
                    }
                }
            }
        }
    }
}";

        var classFile2 = @"
namespace Logic.Readers
{
    public interface IDatabase { }
}
";

        var generated =
@"using System;
using System.Text;

namespace Logic.Readers
{
    using Logic;
    using System.Threading.Tasks;

    namespace Sub1
    {
        using System.Buffers;
        using System.Data;

        namespace Sub2
        {
            using System.Configuration;
            using System.Dynamic;

            public partial class UserReader
            {
                private partial class UpperClass
                {
                    private partial class MiddleClass
                    {
                        private partial class InnerClass
                        {
                            private readonly IDatabase _database;

                            public InnerClass(IDatabase database)
                            {
                                _database = database;
                            }
                        }
                    }
                }
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Sub1.Sub2.UserReader-UpperClass-MiddleClass-InnerClass.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task NestedNamespaceWithAlternateUsingDirectives_GenerateCodeForNested()
    {

        var classFile1 =
@"using System;

namespace Logic.Readers
{
    using Logic;
    using System.Threading.Tasks;
    using Internal = Logic.InternalLogic.Logging;

    namespace Sub1
    {
        using System.Buffers;
        using LRIO = Logic.Readers.IO;

        namespace Sub2
        {
            using System.Configuration;
            using Email = Logic.Communication.IEmailer;

            public partial class UserReader
            {
                private partial class UpperClass
                {
                    private partial class MiddleClass
                    {
                        [SlowFox.InjectDependencies(typeof(LRIO.IDatabase), typeof(Email), typeof(Internal.IEventLog))]
                        private partial class InnerClass
                        {
                        }
                    }
                }
            }
        }
    }
}";

        var classFile2 = @"
namespace Logic.Readers.IO
{
    public interface IDatabase { }
}
namespace Logic.Communication
{
    public interface IEmailer { }
}
namespace Logic.InternalLogic.Logging
{
    public interface IEventLog { }
}
";

        var generated =
@"using System;

namespace Logic.Readers
{
    using Logic;
    using System.Threading.Tasks;
    using Internal = Logic.InternalLogic.Logging;

    namespace Sub1
    {
        using System.Buffers;
        using LRIO = Logic.Readers.IO;

        namespace Sub2
        {
            using System.Configuration;
            using Email = Logic.Communication.IEmailer;

            public partial class UserReader
            {
                private partial class UpperClass
                {
                    private partial class MiddleClass
                    {
                        private partial class InnerClass
                        {
                            private readonly LRIO.IDatabase _database;
                            private readonly Email _email;
                            private readonly Internal.IEventLog _eventLog;

                            public InnerClass(LRIO.IDatabase database, Email email, Internal.IEventLog eventLog)
                            {
                                _database = database;
                                _email = email;
                                _eventLog = eventLog;
                            }
                        }
                    }
                }
            }
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.Sub1.Sub2.UserReader-UpperClass-MiddleClass-InnerClass.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_WithFileNamespace_GenerateMembersAndConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers;

[InjectDependencies(typeof(IDatabase))]
public partial class UserReader { }
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
    public async Task Class_WithAttribute_WithTypeWithAliasAs_I_GenerateMemberAndConstructor()
    {
        var classFile1 =
@"using SlowFox;
using I = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    [InjectDependencies(typeof(I))]
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
using I = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly I _i;

        public UserReader(I i)
        {
            _i = i;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_WithAttribute_WithTypeWithAliasAs_i_GenerateMemberAndConstructor()
    {
        var classFile1 =
@"using SlowFox;
using i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    [InjectDependencies(typeof(i))]
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
using i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly i _i;

        public UserReader(i @i)
        {
            _i = @i;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_WithAttribute_WithTypeWithAliasAs_at_i_GenerateMemberAndConstructor()
    {
        var classFile1 =
@"using SlowFox;
using @i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    [InjectDependencies(typeof(@i))]
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
using @i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly @i _i;

        public UserReader(@i i)
        {
            _i = i;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task Class_WithAttribute_WithTypeWithAliasAs__i_GenerateMemberAndConstructor()
    {
        var classFile1 =
@"using SlowFox;
using _i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    [InjectDependencies(typeof(_i))]
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
using _i = ExternalProject.Helpers.IO.IDatabase;

namespace Logic.Readers
{
    public partial class UserReader
    {
        private readonly _i __i;

        public UserReader(_i @_i)
        {
            __i = @_i;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }

    [Fact]
    public async Task AbstractClass_WithAttribute_GenerateProtectedConstructor()
    {
        var classFile1 =
@"using SlowFox;
using Logic.IO;

namespace Logic.Readers
{
    [InjectDependencies(typeof(IDatabase))]
    public abstract partial class UserReader { }
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
    public abstract partial class UserReader
    {
        private readonly IDatabase _database;

        protected UserReader(IDatabase database)
        {
            _database = database;
        }
    }
}";
        await AssertFullGeneration(generated, "Logic.Readers.UserReader.Generated.cs", classFile1, classFile2);
    }
}

