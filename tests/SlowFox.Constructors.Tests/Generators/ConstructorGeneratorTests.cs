using SlowFox.Constructors.Generators;
using SlowFox.Constructors.Tests.Base;
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
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
            await AssertFullGeneration(generated, "UserReader.Generated.cs", classFile1, classFile2);
        }
    }
}
