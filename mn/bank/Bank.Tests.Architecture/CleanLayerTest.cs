using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bank.Tests.Arch
{
    /// <summary>
    /// Verifies layer dependencies using ArchUnitNet for hierarchical system testing.
    /// </summary>
    /// <remarks> The Clean Architecture comprises four layers:
    /// Domain - Database Model Classes or Application Model Classes
    /// Application - Service Interfaces or ViewModals
    /// Infrastructure - Database Contexts or Migrations or Repositories
    /// Presentation - (aka Api Presentation layer) Front Ends consuming APIs
    /// </remarks>
    public class CleanLayerTest
    {
        /// <summary>
        /// Declares namespaces as immutable for layer dependency evaluation.
        /// </summary>
        private const string SystemNamespace = "Bank";
        private const string DomainNamespace = SystemNamespace + ".Domain";
        private const string DomainCoreNamespace = SystemNamespace + ".Domain.Core";
        private const string ApplicationNamespace = SystemNamespace + ".Application";
        private const string InfrastructureDataNamespace = SystemNamespace + ".Infra.Data";
        private const string PresentationNamespace = SystemNamespace + ".Mvc";

        /// <summary>
        /// Loads two assemblies as Architecture for evaluation.  
        /// First, <ExampleClass>, and second, <ForbiddenClass>, loads types 
        /// from the assemblies.
        /// </summary>
        private static readonly Architecture domainToCoreArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load(DomainNamespace),
                    System.Reflection.Assembly.Load(DomainCoreNamespace)
                )
                .Build();

        private static readonly Architecture domainToAppArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load(DomainNamespace),
                    System.Reflection.Assembly.Load(ApplicationNamespace)
                )
                .Build();

        private static readonly Architecture applicationToPresentationArchitecture =
            new ArchLoader().LoadAssemblies(
                    System.Reflection.Assembly.Load(ApplicationNamespace),
                    System.Reflection.Assembly.Load(PresentationNamespace)
                )
                .Build();

        private static readonly Architecture infraDataToDomainArchitecture = 
            new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(InfrastructureDataNamespace),
                System.Reflection.Assembly.Load(DomainNamespace))
                .Build();

        /// <summary>
        /// Declares variables containing types for evaluation.
        /// </summary>
        /// <remarks>HaveFullNameContaining traverses all qualified identifiers.</remarks>
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
        public void ApplicationLayer_ShouldDependOnPresentation_ReturnsFalse()
        {
            IArchRule applicationLayerShouldAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .DependOnAny(PresentationLayer).Because("It's forbidden.");
            bool checkedRule = applicationLayerShouldAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.False(checkedRule, "Application must not depend on Presentation.");
            //applicationLayerShouldNotAccessPresentationLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotDependOnPresentation_ReturnsTrue()
        {
            IArchRule applicationLayerShouldNotAccessPresentationLayer =
                Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(PresentationLayer).Because("It's required.");
            bool checkedRule = applicationLayerShouldNotAccessPresentationLayer.HasNoViolations(applicationToPresentationArchitecture);
            Assert.True(checkedRule, "Application must not depend on Presentation.");
            //applicationLayerShouldNotAccessPresentationLayer.Check(applicationToPresentationArchitecture);
        }

        [Fact]
        public void DomainLayer_ShouldNotDependOnApplicationLayer_ReturnsTrue()
        {
            IArchRule domainLayerShouldNotAccessApplicationLayer =
                Types().That().Are(DomainLayerFull).Should()
                .NotDependOnAny(ApplicationLayer).Because("It's required.");
            bool checkedRule = domainLayerShouldNotAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.True(checkedRule, "Domain must not depend on Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }

        [Fact]
        public void DomainLayer_ShouldDependOnApplicationLayer_ReturnsFalse()
        {
            IArchRule domainLayerShouldAccessApplicationLayer =
                Types().That().Are(DomainLayerFull).Should()
                .DependOnAny(ApplicationLayer).Because("It's forbidden.");
            bool checkedRule = domainLayerShouldAccessApplicationLayer.HasNoViolations(domainToAppArchitecture);
            Assert.False(checkedRule, "Domain must not depend on Application.");
            //domainLayerShouldNotAccessApplicationLayer.Check(domainToAppArchitecture);
        }

        [Fact]
        public void DomainLayer_ShouldDependOnCore_ReturnsTrue()
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
        public void DomainLayer_ShouldNotDependsOnCore_ReturnsFalse()
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
        public void InfraLayerData_ShouldDependOnDomain_ReturnsTrue()
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
        public void InfraLayerData_ShouldNotDependsOnDomain_ReturnsFalse()
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
    }
}