using Xunit;
using MESArchitecturePoc.Service;
using MESArchitecturePoc.Infra;
using MESArchitecturePoc.Application.Controllers;
using MESArchitecturePoc.Domain.Models;
using ArchUnitNET.xUnit;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using MESArchitecturePoc.Domain.Interfaces;

namespace MESArchitecturePoc.UnitTests
{
    public class ArchUnitNETUnitTests
    {
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(
                typeof(WeatherForecastController).Assembly,
                typeof(WeatherForecastService).Assembly,
                typeof(IWeatherForecastService).Assembly,
                typeof(WeatherForecastAgent).Assembly,
                typeof(IWeatherForecastAgent).Assembly,
                typeof(WeatherForecast).Assembly)
            .Build();

        private readonly IObjectProvider<IType> InfraLayer =
            ArchRuleDefinition.Types().That().ResideInAssembly(typeof(WeatherForecastAgent).Assembly).As("Infra");

        private readonly IObjectProvider<Class> ServiceClasses =
            ArchRuleDefinition.Classes().That().ImplementInterface("IWeatherForecastService").As("WeatherForecastService");

        private readonly IObjectProvider<Class> AgentClasses =
            ArchRuleDefinition.Classes().That().ImplementInterface("IWeatherForecastAgent").As("WeatherForecastAgent");

        private readonly IObjectProvider<IType> DomainLayer =
            ArchRuleDefinition.Types().That().ResideInNamespace("Domain").As("Domain Layer");

        private readonly IObjectProvider<Interface> AgentInterfaces =
            ArchRuleDefinition.Interfaces().That().HaveFullNameContaining("WeatherForecastAgent").As("IWeatherForecastAgent");

        [Fact]
        public void AgentInterfacesShouldBeInDomainLayer()
        {
            IArchRule agentInterfaceShouldBeInDomainLayer =
                ArchRuleDefinition.Interfaces().That().Are(AgentInterfaces).Should().Be(DomainLayer);

            agentInterfaceShouldBeInDomainLayer.Check(Architecture);
        }

        [Fact]
        public void AgentInterfacesShouldNotBeInInfraLayer()
        {
            IArchRule agentInterfaceShouldBeInDomainLayer =
                ArchRuleDefinition.Interfaces().That().Are(AgentInterfaces).Should().NotBe(InfraLayer);

            agentInterfaceShouldBeInDomainLayer.Check(Architecture);
        }

        [Fact]
        public void AgentsShouldBeInInfraLayer()
        {
            IArchRule agentClassesShouldBeInInfraLayer =
                ArchRuleDefinition.Classes().That().Are(AgentClasses).Should().Be(InfraLayer);

            agentClassesShouldBeInInfraLayer.Check(Architecture);
        }

        [Fact]
        public void AgentsShouldNotBeInDomainLayer()
        {
            IArchRule agentClassesShouldBeInInfraLayer =
                ArchRuleDefinition.Classes().That().Are(AgentClasses).Should().NotBe(DomainLayer);

            agentClassesShouldBeInInfraLayer.Check(Architecture);
        }

        [Fact]
        public void InterfacesShouldHaveCorrectName()
        {
            ArchRuleDefinition.Interfaces().Should().HaveNameStartingWith("I")
                .Check(Architecture);
        }

        [Fact]
        public void InterfacesShouldNotHaveIncorrectName()
        {
            ArchRuleDefinition.Interfaces().Should().NotHaveNameStartingWith("W")
                .Check(Architecture);
        }

        [Fact]
        public void ServiceClassesShouldCallInterfacesInDomainLayer()
        {
            ArchRuleDefinition.Classes().That().Are(ServiceClasses).Should().CallAny(
                ArchRuleDefinition.MethodMembers().That().AreDeclaredIn(DomainLayer))
            .Check(Architecture);
        }

        [Fact]
        public void ServiceClassesShouldNotCallConcreteClasses()
        {
            ArchRuleDefinition.Classes().That().Are(ServiceClasses).Should().NotCallAny(
                ArchRuleDefinition.MethodMembers().That().AreDeclaredIn(InfraLayer))
            .Check(Architecture);
        }
    }
}
