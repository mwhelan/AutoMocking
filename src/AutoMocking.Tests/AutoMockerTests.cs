using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMocking.NSubstitute;
using NSubstitute;
using Shouldly;
using Xunit;

namespace AutoMocking.Tests
{
    public class AutoMockerTests
    {
        [Fact]
        public void should_create_sut_with_no_constructor()
        {
            var autoMocker = CreateSut();
            var result = autoMocker.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            result.ShouldBeOfType<ConcreteObjectWithNoConstructor>();
        }

        [Fact]
        public void sut_should_be_a_singleton()
        {
            var autoMocker = CreateSut();
            var expected = autoMocker.SystemUnderTest<ConcreteObjectWithNoConstructor>();

            var result = autoMocker.SystemUnderTest<ConcreteObjectWithNoConstructor>();

            result.ShouldBeSameAs(expected);
        }

        [Fact]
        public void should_create_sut_with_constructors()
        {
            var autoMocker = CreateSut();
            var result = autoMocker.SystemUnderTest<ConcretedObjectWithThreeConstructorArguments>();
            result.ShouldBeOfType<ConcretedObjectWithThreeConstructorArguments>();
        }

        [Fact]
        public void should_create_valid_mocks()
        {
            var autoMocker = CreateSut();
            var sut = autoMocker.SystemUnderTest<ConcretedObjectWithThreeConstructorArguments>();
            
            sut.WriteReport("myreport.md");
            
            autoMocker.Dependency<IReportWriter>().Received().OutputReport(Arg.Any<string>(), "myreport.md");
        }


        private static AutoMockingContainer CreateSut() 
        {
            return new AutoMockingContainer(new NSubstituteDependencyFactory());
        }
    }

    public class ConcreteObjectWithNoConstructor
    {
    }

    public class ConcretedObjectWithThreeConstructorArguments
    {
        private readonly IReportWriter _reportWriter;
        private readonly IDependency2 _dependency2;
        private readonly IDependency3 _dependency3;
        public ConcretedObjectWithThreeConstructorArguments() { }
        public ConcretedObjectWithThreeConstructorArguments(IReportWriter reportWriter, IDependency2 dependency2, IDependency3 dependency3)
        {
            _reportWriter = reportWriter;
            _dependency2 = dependency2;
            _dependency3 = dependency3;
        }

        public void WriteReport(string reportName)
        {
            _reportWriter.OutputReport("report stuff", "myreport.md");
        }
    }

    public interface IReportWriter
    {
        void OutputReport(string reportData, string reportName, string outputDirectory = null);
    }
    public interface IDependency2 { }
    public interface IDependency3 { }
}
