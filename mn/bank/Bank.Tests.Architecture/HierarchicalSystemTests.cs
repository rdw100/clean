using ArchUnitNET.Domain;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bank.Tests.Arch
{
    public class HierarchicalSystemTests
    {
        //private const string SystemNamespace = "Bank";
        //private const string DomainNamespace = SystemNamespace + ".Domain";
        //private const string ApplicationNamespace = SystemNamespace + ".Application";
        //private const string InfrastructureNamespace = SystemNamespace + ".Infra";
        //private const string PresentationNamespace = SystemNamespace + ".Ui";

        //TIP: load your architecture once at the start to maximize performance of your tests
        private static readonly Architecture architectureDomain = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Domain.Core"),
                System.Reflection.Assembly.Load("Bank.Domain"))
            .Build();
        // replace <ExampleClass> and <ForbiddenClass> with classes from the assemblies you want to test

        private readonly IObjectProvider<IType> Domain =
            Types().That().ResideInAssembly("Bank.Domain").As("Domain Layer");

        private readonly IObjectProvider<IType> DomainCore =
            Types().That().ResideInAssembly("Bank.Domain.Core").As("Domain Layer - Core");

        [Fact]
        public void DomainCoreDependsOnNothingTest()
        {
            IArchRule rule = Types().That().ResideInNamespace("Bank.Domain.Core").Should()
                    .NotDependOnAny(Types().That().ResideInNamespace("Bank.Domain"));

            bool checkedRule = rule.HasNoViolations(architectureDomain);
            Assert.True(checkedRule, "Domain Core must not reference any other projects.");
        }

        private static readonly Architecture architectureApplication = new ArchLoader().LoadAssemblies(
        System.Reflection.Assembly.Load("Bank.Application"))
            .Build();

        private readonly IObjectProvider<IType> Application =
            Types().That().ResideInAssembly("Bank.Application").As("Application Layer");

        private readonly IObjectProvider<IType> Presentation =
            Types().That().ResideInAssembly("Bank.Mvc").As("Presentation Layer");

        [Fact]
        public void ApplicationShouldNotAccessPresentation()
        {
            IArchRule applicationLayerShouldNotAccessPresentation = Types().That().Are(Application).Should()
                .NotDependOnAny(Presentation).Because("it's forbidden");
            applicationLayerShouldNotAccessPresentation.Check(architectureApplication);
        }

        [Fact]
        public void ApplicationServicesShouldAccessDomain() 
        {
            IEnumerable<string> moreTypes = new[]
            {
                "Bank.Domain.Core.Bus",
                "Bank.Domain.Commands",
                "Bank.Domain.Interface"
            };

            IArchRule domainShouldNotAccessOtherLayers =
                Types()
                .That()
                .ResideInAssembly("Bank.Application.Services")
                .Should()
                .DependOnAny(moreTypes);

            domainShouldNotAccessOtherLayers.Check(architectureApplication);
        }

        [Fact]
        public void ApplicationServicesShouldNotAccessModels()
        {
            IEnumerable<string> moreTypes = new[]
            {
                "Bank.Domain.Models"
            };

            IArchRule domainShouldNotAccessOtherLayers =
                Types()
                .That()
                .ResideInAssembly("Bank.Application.Services")
                .Should()
                .NotDependOnAny(moreTypes);

            domainShouldNotAccessOtherLayers.Check(architectureApplication);
        }

        [Fact]
        public void ApplicationViewModelsShouldAccessModels()
        {
            IEnumerable<string> moreTypes = new[]
            {
                "Bank.Domain.Models"
            };

            IArchRule domainShouldNotAccessOtherLayers =
                Types()
                .That()
                .ResideInNamespace("Bank.Application.ViewModels.AccountViewModel")
                .Should()
                .DependOnAny(moreTypes);

            domainShouldNotAccessOtherLayers.Check(architectureApplication);
        }

        private static readonly Architecture architectureInfrastructureBus = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Infra.Bus"))
            .Build();

        private static readonly Architecture architectureInfrastructureData = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Infra.Data"))
            .Build();

        private static readonly Architecture architectureInfrastructureIoC = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Infra.IoC"))
            .Build();
    }
}