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
        private const string ApplicationInterfaces = ApplicationNamespace + ".Interfaces";
        private const string ApplicationViewModels = ApplicationNamespace + ".ViewModels";
        private const string ApplicationServices = ApplicationNamespace + ".Services";
        private const string DomainNamespace = SystemNamespace + ".Domain";
        private const string DomainModels = DomainNamespace + ".Models";
        private const string PresentationApiControllers = SystemNamespace + "Api.Controllers";
        private const string PresentationMvcNamespace = SystemNamespace + ".Mvc";
        private const string PresentationMvcControllers = SystemNamespace + ".Mvc.Controllers";

        ////TIP: load your architecture once at the start to maximize performance of your tests
        //private static readonly Architecture architectureDomain =
        //    new ArchLoader().LoadAssemblies(
        //        System.Reflection.Assembly.Load("Bank.Domain.Core"),
        //        System.Reflection.Assembly.Load("Bank.Domain")
        //    ).Build();

        private static readonly Architecture applicationToDomainArchitecture = 
            new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(ApplicationNamespace),
                System.Reflection.Assembly.Load(DomainNamespace)
            ).Build();
        
        private static readonly Architecture presentationArchitecture =
            new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(PresentationMvcNamespace),
                System.Reflection.Assembly.Load(ApplicationNamespace)
            ).Build();

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

        [Fact]
        public void ApplicationServices_ShouldNotAccessModels_ReturnsTrue()
        {
            IArchRule servicesShouldNotAccessModles =
                Types()
                .That()
                .ResideInNamespace(ApplicationServices)
                .Should()
                .NotDependOnAny(Types().That().ResideInNamespace(DomainModels));

            bool checkedRule = servicesShouldNotAccessModles.HasNoViolations(applicationToDomainArchitecture);
            Assert.True(checkedRule, "Services should not access Models.");
            //servicesShouldNotAccessModles.Check(applicationToDomainArchitecture);
        }

        [Fact]
        public void ApplicationServices_ShouldAccessModels_ReturnsFalse()
        {
            IArchRule servicesShouldAccessModles =
                Types()
                .That()
                .ResideInNamespace(ApplicationServices)
                .Should()
                .DependOnAny(Types().That().ResideInNamespace(DomainModels));

            bool checkedRule = servicesShouldAccessModles.HasNoViolations(applicationToDomainArchitecture);
            Assert.False(checkedRule, "Services should not access Models.");
            //servicesShouldAccessModles.Check(applicationToDomainArchitecture);
        }

        [Fact]
        public void ViewModels_ShouldAccessModels_ReturnsTrue()
        {
            IArchRule shouldAccessModels =
                Types()
                .That()
                .ResideInNamespace(ApplicationViewModels)
                .Should()
                .DependOnAny(
                    Types().That().ResideInNamespace(DomainModels)
                 );

            bool checkedRule = shouldAccessModels.HasNoViolations(applicationToDomainArchitecture);
            Assert.True(checkedRule, "ViewModels should access Models.");
            //shouldAccessModels.Check(architectureApplication);
        }

        [Fact]
        public void ViewModels_ShouldNotAccessModels_ReturnsFalse()
        {
            IArchRule shouldNotAccessModels =
                Types()
                .That()
                .ResideInNamespace(ApplicationViewModels)
                .Should()
                .NotDependOnAny(
                    Types().That().ResideInNamespace(DomainModels)
                 );

            bool checkedRule = shouldNotAccessModels.HasNoViolations(applicationToDomainArchitecture);
            Assert.False(checkedRule, "ViewModels should access Models.");
            //shouldAccessModels.Check(architectureApplication);
        }

        [Fact]
        public void Controllers_ShouldBeAuthorized_ReturnTrue()
        {
            IArchRule shouldBeAuthorized = Classes()
                .That()
                .ResideInNamespace(PresentationMvcControllers)
                .And()
                .DependOnAny(
                    Types().That().ResideInNamespace(ApplicationInterfaces)
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
                .ResideInNamespace(PresentationMvcControllers)
                .And()
                .DependOnAny(
                    Types().That().ResideInNamespace(ApplicationInterfaces)
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