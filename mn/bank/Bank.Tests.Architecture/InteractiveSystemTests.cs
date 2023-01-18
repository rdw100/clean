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
        //TIP: load your architecture once at the start to maximize performance of your tests
        private static readonly Architecture architecture = new ArchLoader().LoadAssemblies(
                System.Reflection.Assembly.Load("Bank.Domain"),
                System.Reflection.Assembly.Load("Bank.Mvc"))
            .Build();
        // replace <ExampleClass> and <ForbiddenClass> with classes from the assemblies you want to test

        [Fact]
        public void ModelDependsOnNothingTest()
        {
            // Namespace Dependency Rule
            IArchRule rule = Types().That().ResideInNamespace("Bank.Domain.Models").Should()
                    .NotDependOnAny(Types().That().ResideInNamespace("Bank.Mvc.Controllers"));

            bool checkedRule = rule.HasNoViolations(architecture);
            Assert.True(checkedRule, "Model Project must not reference any other projects.");
        }
    }
}