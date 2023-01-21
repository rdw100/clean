using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bank.Tests.Arch
{
    public class CleanLayerTest
    {
        private static readonly Architecture domainCoreArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load("Bank.Domain")
                )
                .Build();

        private static readonly Architecture domainToCoreArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load("Bank.Domain"),
                    System.Reflection.Assembly.Load("Bank.Domain.Core")
                )
                .Build();

        private static readonly Architecture domainToAppArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load("Bank.Domain"),
                    System.Reflection.Assembly.Load("Bank.Application")
                )
                .Build();

        private static readonly Architecture applicationToPresentationArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load("Bank.Application"),
                    System.Reflection.Assembly.Load("Bank.Mvc")
                )
                .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>HaveFullNameContaining traverses qualified identifiers.</remarks>
        private readonly IObjectProvider<IType> DomainLayer =
            Types().That().HaveFullNameContaining("Bank.Domain").As("Domain Layer");

        private readonly IObjectProvider<IType> DomainCoreLayer =
            Types().That().HaveFullNameContaining("Bank.Domain.Core").As("Domain Core Layer");

        private readonly IObjectProvider<IType> ApplicationLayer =
            Types().That().HaveFullNameContaining("Bank.Application").As("Application Layer");

        private readonly IObjectProvider<IType> PresentationLayer =
            Types().That().HaveFullNameContaining("Bank.Mvc").As("Presentation Layer");

        //Types().That().ResideInNamespace("Bank.Domain.Core").As("Forbidden Layer");

        //private readonly IObjectProvider<IType> Domain =
        //    Types().That().ResideInAssembly("Bank.Domain").As("Domain Layer");

        //private readonly IObjectProvider<IType> DomainCore =
        //    Types().That().ResideInAssembly("Bank.Domain.Core").As("Domain Layer - Core");

        //private readonly IObjectProvider<IType> Application =
        //    Types().That().ResideInAssembly("Bank.Application").As("Application Layer");

        //private readonly IObjectProvider<IType> Presentation =
        //    Types().That().ResideInAssembly("Bank.Mvc").As("Presentation Layer");

        [Fact]
        public void ApplicationLayer_DoesNotDependOnPresentation_ReturnsTrue()
        {
            IArchRule applicationLayerShouldNotAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(PresentationLayer).Because("It's required.");
            bool checkedRule = applicationLayerShouldNotAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.True(checkedRule, "Domain must only depend on Domain Core.");
            //exampleLayerShouldNotAccessForbiddenLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void ApplicationLayer_DoesNotDependOnPresentation_ReturnsFalse()
        {
            IArchRule applicationLayerShouldNotAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .DependOnAny(PresentationLayer).Because("It's forbidden.");
            bool checkedRule = applicationLayerShouldNotAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.False(checkedRule, "Domain must only depend on Domain Core.");
            //exampleLayerShouldNotAccessForbiddenLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void DomainLayer_DoesNotDependOnApplicationLayer_ReturnsTrue()
        {
            IArchRule domainLayerShouldNotAccessApplicationLayer =
                Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(ApplicationLayer).Because("It's required.");            
            bool checkedRule = domainLayerShouldNotAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.True(checkedRule, "Domain must not access Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }

        [Fact]
        public void DomainLayer_DoesNotDependOnApplicationLayer_ReturnsFalse()
        {
            IArchRule domainLayerShouldNotAccessApplicationLayer =
                Types().That().Are(DomainLayer).Should()
                .DependOnAny(ApplicationLayer).Because("It's forbidden.");
            bool checkedRule = domainLayerShouldNotAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.False(checkedRule, "Domain must not access Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }
    }
}