using ArchUnitNET.xUnit;
using Bank.Application.Interfaces;
using Bank.Domain.Interface;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bank.Tests.Arch
{
    /// <summary>
    /// Tests interactive systems that support user interations with user interfaces.
    /// </summary>
    /// <example
    /// <remarks>Interactive systems are systems that support user interations with user interfaces like the MVC pattern.</remarks>
    public class InteractiveSystemTests
    {
        private const string SystemNamespace = "Bank";
        private const string ApplicationNamespace = SystemNamespace + ".Application";
        private const string DomainNamespace = SystemNamespace + ".Domain";
        private const string DomainModels = DomainNamespace + ".Models";
        private const string PresentationApiControllers = SystemNamespace + "Api.Controllers";
        private const string PresentationApiNamespace = SystemNamespace + ".Api";
        private const string PresentationMvcControllers = SystemNamespace + "Mvc.Controllers";
        private const string PresentationMvcNamespace = SystemNamespace + ".Mvc";
        private const string InfrastructureDataNamespace = SystemNamespace + ".Infra.Data";

        private static readonly Architecture mvcArchitecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(DomainNamespace),
                System.Reflection.Assembly.Load(PresentationMvcNamespace)
            ).Build();

        private static readonly Architecture apiArchitecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(DomainNamespace),
                System.Reflection.Assembly.Load(PresentationApiNamespace)
            ).Build();

        private static readonly Architecture applicationArchitecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(ApplicationNamespace)
            ).Build();

        private static readonly Architecture domainArchitecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(DomainNamespace)
            ).Build();

        private static readonly Architecture infraDataArchitecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load(InfrastructureDataNamespace)
            ).Build();

        /// <summary>
        /// Tests namespace dependency rule where models cannot depend on controllers.
        /// </summary>
        [Fact]
        public void Models_ShouldNotDependOnMvcControllers_ReturnsTrue()
        {
            IArchRule shouldNotDependOnMvcControllers = Types()
                .That()
                .ResideInNamespace(DomainModels)
                .Should()
                .NotDependOnAny(
                        Types().That().ResideInNamespace(PresentationMvcControllers)
                    );

            bool checkedRule = shouldNotDependOnMvcControllers.HasNoViolations(mvcArchitecture);
            Assert.True(checkedRule, "Models must not depend on controllers.");
            //shouldNotDependOnMvcControllers.Check(mvcArchitecture);
        }

        // Namespace Dependency Rule
        [Fact]
        public void Models_ShouldDependOnMvcControllers_ReturnsFalse()
        {
            IArchRule shouldDependOnMvcControllers = Types()
                .That()
                .ResideInNamespace(DomainModels)
                .Should()
                .DependOnAny(
                        Types().That().ResideInNamespace(PresentationMvcControllers)
                    );

            bool checkedRule = shouldDependOnMvcControllers.HasNoViolations(mvcArchitecture);
            Assert.False(checkedRule, "Models must not depend on controllers.");
            //shouldDependOnMvcControllers.Check(mvcArchitecture);
        }

        // Namespace Dependency Rule
        [Fact]
        public void Models_ShouldNotDependOnApiControllers_ReturnsTrue()
        {
            IArchRule shouldNotDependOnApiControllers = Types()
                .That()
                .ResideInNamespace(DomainModels)
                .Should()
                .NotDependOnAny(
                        Types().That().ResideInNamespace(PresentationApiControllers)
                    );

            bool checkedRule = shouldNotDependOnApiControllers.HasNoViolations(apiArchitecture);
            Assert.True(checkedRule, "Models must not depend on controllers.");
            //shouldNotDependOnApiControllers.Check(apiArchitecture);
        }

        // Namespace Dependency Rule
        [Fact]
        public void Models_ShouldNotDependOnApiControllers_ReturnsFalse()
        {
            IArchRule shouldNotDependOnApiControllers =
                Types()
                .That()
                .ResideInNamespace(DomainModels)
                .Should()
                .NotDependOnAny(
                        Types().That().ResideInNamespace(PresentationApiControllers)
                    );

            bool checkedRule = shouldNotDependOnApiControllers.HasNoViolations(apiArchitecture);
            Assert.True(checkedRule, "Models must not depend on controllers.");
            //shouldNotDependOnApiControllers.Check(apiArchitecture);
        }

        /// <summary>
        ///  Inheritance Naming Rule
        /// </summary>
        [Fact]
        public void Services_ShouldBeNamedService_ReturnsTrue()
        {
            IArchRule shouldBeNamedService = Classes()
                .That()
                .AreAssignableTo(typeof(IAccountService))
                .Should()
                .HaveNameContaining("Service");

            bool checkedRule = shouldBeNamedService.HasNoViolations(applicationArchitecture);
            Assert.True(checkedRule, "A service should be named service.");
            //shouldBeNamedService.Check(applicationArchitecture);
        }

        ///  Inheritance Naming Rule
        [Fact]
        public void Services_ShouldNotBeNamedService_ReturnsFalse()
        {
            IArchRule shouldNotBeNamedService = Classes()
                .That()
                .AreAssignableTo(typeof(IAccountService))
                .Should()
                .NotHaveNameContaining("Service");
            bool checkedRule = shouldNotBeNamedService.HasNoViolations(applicationArchitecture);
            Assert.False(checkedRule, "A service should be named service.");
            //shouldNotBeNamedService.Check(applicationArchitecture);
        }

        ///  Inheritance Naming Rule
        [Fact]
        public void Repository_ShouldBeNamedRepository_ReturnsTrue()
        {
            IArchRule shouldBeNamedRepository = Classes()
                .That()
                .AreAssignableTo(typeof(IAccountRepository))
                .Should()
                .HaveNameContaining("Repository");
            bool checkedRule = shouldBeNamedRepository.HasNoViolations(infraDataArchitecture);
            Assert.True(checkedRule, "A repository should be named repository.");
            //shouldBeNamedRepository.Check(infraDataArchitecture);
        }

        ///  Inheritance Naming Rule
        [Fact]
        public void Repository_ShouldNotBeNamedRepository_ReturnsFalse()
        {
            IArchRule shouldNotBeNamedRepository = Classes()
                .That()
                .AreAssignableTo(typeof(IAccountRepository))
                .Should()
                .NotHaveNameContaining("Repository");
            bool checkedRule = shouldNotBeNamedRepository.HasNoViolations(infraDataArchitecture);
            Assert.False(checkedRule, "A repository should be named repository.");
            //shouldNotBeNamedRepository.Check(infraDataArchitecture);
        }
    }
}