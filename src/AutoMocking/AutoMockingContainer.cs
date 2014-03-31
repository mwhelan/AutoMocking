using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMocking
{
    public class AutoMockingContainer : IContainer
    {
        private readonly IDictionary<Type, object> _dependencies = new Dictionary<Type, object>();
        private readonly IDependencyFactory _dependencyFactory;
        private object _sut;

        public AutoMockingContainer(IDependencyFactory dependencyFactory)
        {
            _dependencyFactory = dependencyFactory;
        }

        public T SystemUnderTest<T>() where T : class
        {
            if (_sut == null)
            {
                _sut = CreateSystemUnderTest<T>();
            }

            return (T)_sut;
        }

        public T Dependency<T>()
        {
            return (T)Dependency(typeof(T));
        }

        public object Dependency(Type dependencyType)
        {
            if (!_dependencies.ContainsKey(dependencyType))
            {
                _dependencies.Add(dependencyType, _dependencyFactory.CreateDependency(dependencyType));
            }

            return _dependencies[dependencyType];
        }

        public void InjectDependency<T>(T dependency)
        {
            _dependencies.Add(typeof(T), dependency);
        }

        private T CreateSystemUnderTest<T>()
        {
            var constructor = GreediestConstructor<T>();
            var constructorParameters = new List<object>();

            foreach (var parameterInfo in constructor.GetParameters())
            {
                constructorParameters.Add(Dependency(parameterInfo.ParameterType));
            }

            return (T)constructor.Invoke(constructorParameters.ToArray());
        }

        private ConstructorInfo GreediestConstructor<T>()
        {
            return typeof(T).GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
        }
    }
}