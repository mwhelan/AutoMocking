using System;

namespace AutoMocking
{
    public interface IContainer
    {
        T Dependency<T>();
        object Dependency(Type dependencyType);
        T SystemUnderTest<T>() where T : class;
        void InjectDependency<T>(T dependency);
    }
}