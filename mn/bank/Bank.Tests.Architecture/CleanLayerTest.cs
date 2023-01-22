using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bank.Tests.Arch
{
    public class CleanLayerTest
    {
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

        private static readonly Architecture infraDataToDomainArchitecture = 
            new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Infra.Data"),
                System.Reflection.Assembly.Load("Bank.Domain"))
                .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>HaveFullNameContaining traverses qualified identifiers.</remarks>
        private readonly IObjectProvider<IType> DomainLayerFull =
            Types().That().HaveFullNameContaining("Bank.Domain").As("Domain Layer");

        private readonly IObjectProvider<IType> DomainCoreLayerFull =
            Types().That().HaveFullNameContaining("Bank.Domain.Core").As("Domain Layer Core");

        private readonly IObjectProvider<IType> ApplicationLayer =
            Types().That().HaveFullNameContaining("Bank.Application").As("Application Layer");

        private readonly IObjectProvider<IType> PresentationLayer =
            Types().That().HaveFullNameContaining("Bank.Mvc").As("Presentation Layer");

        private readonly IObjectProvider<IType> InfraLayerDataFull =
            Types().That().HaveFullNameContaining("Bank.Infra.Data").As("Infrastructure Layer - Data");

        [Fact]
        public void InfraLayerData_DependsOnDomain_ReturnsTrue()
        {
            IArchRule infraDataShouldAccessDomain =
                Types().That().Are(InfraLayerDataFull).Should()
                .DependOnAny(DomainLayerFull).OrShould()
                .DependOnAny("^System.", true).OrShould()
                .DependOnAny("^Microsoft.", true)
                .Because("It's required.");
            bool checkedRule = infraDataShouldAccessDomain.HasNoViolations(infraDataToDomainArchitecture);
            Assert.True(checkedRule, "Infra Data must depend on Domain.");
            //infraDataShouldAccessDomain.Check(infraDataToDomainArchitecture);
        }

        [Fact]
        public void InfraLayerData_DoesNotDependsOnDomain_ReturnsFalse()
        {
            IArchRule infraDataShouldNotAccessDomain =
                Types().That().Are(InfraLayerDataFull).Should()
                .NotDependOnAny(DomainLayerFull).OrShould()
                .NotDependOnAny("^System.", true).OrShould()
                .NotDependOnAny("^Microsoft.", true)
                .Because("It's required.");
            bool checkedRule = infraDataShouldNotAccessDomain.HasNoViolations(infraDataToDomainArchitecture);
            Assert.False(checkedRule, "Infra Data must depend on Domain.");
            //infraDataShouldNotAccessDomain.Check(infraDataToDomainArchitecture);
        }

        [Fact]
        public void DomainLayer_DependsOnCore_ReturnsTrue()
        {
            IArchRule domainShouldAccessDomainCore =
                Types().That().Are(DomainLayerFull).Should()
                .DependOnAny(DomainCoreLayerFull).OrShould()
                .DependOnAny("^System.", true)
                .Because("It's required.");
            bool checkedRule = domainShouldAccessDomainCore.HasNoViolations(domainToCoreArchitecture);
            Assert.True(checkedRule, "Domain must depend on Domain Core.");
            //domainShouldAccessDomainCore.Check(domainToCoreArchitecture);
        }

        [Fact]
        public void DomainLayer_DoesNotDependsOnCore_ReturnsFalse()
        {
            IArchRule domainShouldNotAccessDomainCore =
                Types().That().Are(DomainLayerFull).Should()
                .NotDependOnAny(DomainCoreLayerFull).OrShould()
                .NotDependOnAny("^System.", true)
                .Because("It's required.");
            bool checkedRule = domainShouldNotAccessDomainCore.HasNoViolations(domainToCoreArchitecture);
            Assert.False(checkedRule, "Domain must depend on Domain Core.");
            //domainShouldNotAccessDomainCore.Check(domainToCoreArchitecture);
        }

        [Fact]
        public void ApplicationLayer_DoesNotDependOnPresentation_ReturnsTrue()
        {
            IArchRule applicationLayerShouldNotAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(PresentationLayer).Because("It's required.");
            bool checkedRule = applicationLayerShouldNotAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.True(checkedRule, "Application must not depend on Presentation.");
            //exampleLayerShouldNotAccessForbiddenLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void ApplicationLayer_DependsOnPresentation_ReturnsFalse()
        {
            IArchRule applicationLayerShouldAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .DependOnAny(PresentationLayer).Because("It's forbidden.");
            bool checkedRule = applicationLayerShouldAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.False(checkedRule, "Application must not depend on Presentation.");
            //exampleLayerShouldNotAccessForbiddenLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void DomainLayer_DoesNotDependOnApplicationLayer_ReturnsTrue()
        {
            IArchRule domainLayerShouldNotAccessApplicationLayer =
                Types().That().Are(DomainLayerFull).Should()
                .NotDependOnAny(ApplicationLayer).Because("It's required.");            
            bool checkedRule = domainLayerShouldNotAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.True(checkedRule, "Domain must not access Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }

        [Fact]
        public void DomainLayer_DependsOnApplicationLayer_ReturnsFalse()
        {
            IArchRule domainLayerShouldAccessApplicationLayer =
                Types().That().Are(DomainLayerFull).Should()
                .DependOnAny(ApplicationLayer).Because("It's forbidden.");
            bool checkedRule = domainLayerShouldAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.False(checkedRule, "Domain must not access Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }
    }
}