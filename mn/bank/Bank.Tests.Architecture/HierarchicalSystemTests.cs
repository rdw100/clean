using ArchUnitNET.xUnit;
using Bank.Application.Interfaces;
using Bank.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
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

        private const string SystemNamespace = "Bank";
        private const string ApplicationNamespace = SystemNamespace + ".Application";
        private const string DomainNamespace = SystemNamespace + ".Domain";
        private const string DomainModels = DomainNamespace + ".Models";
        private const string PresentationApiControllers = SystemNamespace + "Api.Controllers";
        private const string PresentationMvcNamespace = SystemNamespace + ".Mvc";

        ////TIP: load your architecture once at the start to maximize performance of your tests
        //private static readonly Architecture architectureDomain =
        //    new ArchLoader().LoadAssemblies(
        //        System.Reflection.Assembly.Load("Bank.Domain.Core"),
        //        System.Reflection.Assembly.Load("Bank.Domain")
        //    ).Build();

        private static readonly Architecture presentationArchitecture =
            new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(PresentationMvcNamespace),
                System.Reflection.Assembly.Load(ApplicationNamespace)
            ).Build();

        //[Fact]
        //public void DomainShouldNotAccessDomainCore()
        //{

        //    IArchRule domainShouldNotAccessCore =
        //        Types().That().Are(Application).Should()
        //        .NotDependOnAny(Presentation).Because("it's forbidden");

        //    domainShouldNotAccessCore.Check(architectureDomain);
        //}


        //// replace <ExampleClass> and <ForbiddenClass> with classes from the assemblies you want to test

        //private static readonly Architecture architectureApplication = new ArchLoader().LoadAssemblies(
        //    System.Reflection.Assembly.Load("Bank.Application"))
        //    .Build();

        //private static readonly Architecture architectureInfrastructureBus = new ArchLoader().LoadAssemblies(
        //    System.Reflection.Assembly.Load("Bank.Infra.Bus"))
        //    .Build();

        //private static readonly Architecture architectureInfrastructureData = new ArchLoader().LoadAssemblies(
        //    System.Reflection.Assembly.Load("Bank.Infra.Data"))
        //    .Build();

        //private static readonly Architecture architectureInfrastructureIoC = new ArchLoader().LoadAssemblies(
        //    System.Reflection.Assembly.Load("Bank.Infra.IoC"))
        //    .Build();

        //private readonly IObjectProvider<IType> Domain =
        //    Types().That().ResideInAssembly("Bank.Domain").As("Domain Layer");

        //private readonly IObjectProvider<IType> DomainCore =
        //    Types().That().ResideInAssembly("Bank.Domain.Core").As("Domain Layer - Core");

        //private readonly IObjectProvider<IType> Application =
        //    Types().That().ResideInAssembly("Bank.Application").As("Application Layer");

        //private readonly IObjectProvider<IType> Presentation =
        //    Types().That().ResideInAssembly("Bank.Mvc").As("Presentation Layer");

        //    //IObjectProvider<IType>[] moreTypes = new[]
        //    //{
        //    //    ApplicationLayer,
        //    //    DomainLayer
        //    //};

        //[Fact]
        //public void Domain_OnlyDependsOnCore_Test()       
        //{
        //    IArchRule rule = Types()
        //        .That()
        //        .ResideInNamespace("Bank.Domain")
        //        .Should()
        //        .OnlyDependOnTypesThat()
        //        .ResideInNamespace("Bank.Domain.Core");

        //    bool checkedRule = rule.HasNoViolations(architectureDomain);
        //    Assert.True(checkedRule, "Domain must only depend on Domain Core.");
        //}

        //[Fact]
        //public void ApplicationShouldNotAccessPresentation()
        //{
        //    IArchRule applicationLayerShouldNotAccessPresentation = Types().That().Are(Application).Should()
        //        .NotDependOnAny(Presentation).Because("it's forbidden");
        //    applicationLayerShouldNotAccessPresentation.Check(architectureApplication);
        //}

        //[Fact]
        //public void ApplicationServicesShouldAccessDomain() 
        //{
        //    IEnumerable<string> moreTypes = new[]
        //    {
        //        "Bank.Domain.Core.Bus",
        //        "Bank.Domain.Commands",
        //        "Bank.Domain.Interface"
        //    };

        //    IArchRule domainShouldNotAccessOtherLayers =
        //        Types()
        //        .That()
        //        .ResideInAssembly("Bank.Application.Services")
        //        .Should()
        //        .DependOnAny(moreTypes);

        //    domainShouldNotAccessOtherLayers.Check(architectureApplication);
        //}

        //[Fact]
        //public void ApplicationServicesShouldNotAccessModels()
        //{
        //    IEnumerable<string> moreTypes = new[]
        //    {
        //        "Bank.Domain.Models"
        //    };

        //    IArchRule domainShouldNotAccessOtherLayers =
        //        Types()
        //        .That()
        //        .ResideInAssembly("Bank.Application.Services")
        //        .Should()
        //        .NotDependOnAny(moreTypes);

        //    domainShouldNotAccessOtherLayers.Check(architectureApplication);
        //}

        //[Fact]
        //public void ApplicationViewModelsShouldAccessModels()
        //{
        //    IEnumerable<string> moreTypes = new[]
        //    {
        //        "Bank.Domain.Models"
        //    };

        //    IArchRule domainShouldNotAccessOtherLayers =
        //        Types()
        //        .That()
        //        .ResideInNamespace("Bank.Application.ViewModels.AccountViewModel")
        //        .Should()
        //        .DependOnAny(moreTypes);

        //    domainShouldNotAccessOtherLayers.Check(architectureApplication);
        //}

        [Fact]
        public void Controllers_ShouldBeAuthorized_ReturnTrue()
        {
            IArchRule shouldBeAuthorized = Classes()
                .That()
                .ResideInNamespace("Bank.Mvc.Controllers")
                .And()
                .DependOnAny(
                    Types().That().ResideInNamespace("Bank.Application.Interfaces")
                )
                .Should()
                .HaveAnyAttributesThat()
                .Are(typeof(AuthorizeAttribute));

            bool checkedRule = shouldBeAuthorized.HasNoViolations(presentationArchitecture);
            Assert.True(checkedRule, "An MVC controller should be authorized.");
            //shouldBeAuthorized.Check(presentationArchitecture);
        }

        [Fact]
        public void Controllers_ShouldNotBeAuthorized_ReturnFalse()
        {
            IArchRule shouldNotBeAuthorized = Classes()
                .That()                
                .ResideInNamespace("Bank.Mvc.Controllers")
                .And()
                .DependOnAny(
                    Types().That().ResideInNamespace("Bank.Application.Interfaces")
                )
                .Should()
                .NotHaveAnyAttributesThat()
                .Are(typeof(AuthorizeAttribute));

            bool checkedRule = shouldNotBeAuthorized.HasNoViolations(presentationArchitecture);
            Assert.False(checkedRule, "An MVC controller should be authorized.");
            //shouldNotBeAuthorized.Check(presentationArchitecture);
        }
    }
}