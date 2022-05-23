using SlowFox.Core.Logic;

namespace SlowFox.Core.Tests.Logic
{
    public class NameGeneratorTests
    {
        [Theory]
        [InlineData("IDatabase", "database")]
        [InlineData("Database", "database")]
        [InlineData("", "")]
        [InlineData("IO.Logic.Database", "database")]
        [InlineData("IO.Logic.IDatabase", "database")]
        [InlineData("DATABASE", "database")]
        [InlineData("IDATABASE", "idatabase")]
        [InlineData("database", "database")]
        [InlineData("IDatabase?", "database")]
        [InlineData("IList<IDatabase>", "databaseList")]
        [InlineData("List<IDatabase>", "databaseList")]
        [InlineData("ICollection<IDatabase>", "databaseCollection")]
        [InlineData("Collection<IDatabase>", "databaseCollection")]
        [InlineData("IRepository<User>", "userRepository")]
        [InlineData("InnerRepository<User>", "userInnerRepository")]
        [InlineData("IInnerRepository<User>", "userInnerRepository")]
        [InlineData("IRepository<User, Group>", "userGroupRepository")]
        [InlineData("IRepository<User, Group, Address, Window>", "userGroupAddressWindowRepository")]
        [InlineData("(User, Address)", "userAddress")]
        [InlineData("(IUser, IAddress)", "userAddress")]
        [InlineData("(IUser, IRepository<Address>)", "userAddressRepository")]
        [InlineData("(IUser firstUser, IAddress firstUserAddress)", "userAddress")]
        [InlineData("IRepository<(IUser, IAddress)>", "userAddressRepository")]
        [InlineData("IRepository<(IUser firstUser, IAddress firstUserAddress)>", "userAddressRepository")]
        [InlineData("IRepository<User, ICollection<Group>>", "userGroupCollectionRepository")]
        [InlineData("IRepository<User, ICollection<(Group, Address)>>", "userGroupAddressCollectionRepository")]
        [InlineData("IRepository<User, ICollection<(Group group, Address address)>>", "userGroupAddressCollectionRepository")]
        [InlineData("(IUser, IRepository<(Address, NestedStore<Branch>)>)", "userAddressBranchNestedStoreRepository")]
        public void GetName_GenerateValidName(string typeName, string expectedName)
        {
            var usedNames = new List<string>();
            
            var generatedName = NameGenerator.GetName(typeName, usedNames);

            Assert.Equal(expectedName, generatedName);
        }
    }
}
