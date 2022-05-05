﻿using I = ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.BaseClasses
{
    public abstract class AbstractBaseClassWithDependencyAliasNamespace
    {
        private readonly I.IUserReader _userReader;

        public AbstractBaseClassWithDependencyAliasNamespace(I.IUserReader userReader)
        {
            _userReader = userReader;
        }

        public I.IUserReader UserReader => _userReader;
    }
}
