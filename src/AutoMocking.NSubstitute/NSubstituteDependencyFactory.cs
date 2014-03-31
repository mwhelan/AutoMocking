using System;
using NSubstitute;

namespace AutoMocking.NSubstitute
{
    public class NSubstituteDependencyFactory : IDependencyFactory
    {
        public T CreateDependency<T>(params object[] constructorArguments) where T : class
        {
            return Substitute.For<T>(constructorArguments);
        }

        public object CreateDependency(Type type, params object[] constructorArguments)
        {
            return Substitute.For(new[] { type }, constructorArguments);
        }
    }
}