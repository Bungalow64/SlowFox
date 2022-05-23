using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net5.Constructors.Sample.BaseClasses
{
    public abstract class AbstractBaseClassWithMultipleDependencies
    {
        private readonly IUserReader _userReader;
        private readonly IUserWriter _userWriter;

        public AbstractBaseClassWithMultipleDependencies(IUserReader userReader, IUserWriter userWriter)
        {
            _userReader = userReader;
            _userWriter = userWriter;
        }

        public IUserReader UserReader => _userReader;
        public IUserWriter UserWriter => _userWriter;
    }
}
