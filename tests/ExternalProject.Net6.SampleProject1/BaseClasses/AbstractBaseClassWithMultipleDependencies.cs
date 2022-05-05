using ExternalProject.Net6.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net6.SampleProject1.BaseClasses
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
