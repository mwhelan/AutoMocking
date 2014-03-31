using System;

namespace AutoMocking
{
    public interface IDependencyFactory
    {
        T CreateDependency<T>(params object[] constructorArguments) where T : class;

        object CreateDependency(Type type, params object[] constructorArguments);
    }
}