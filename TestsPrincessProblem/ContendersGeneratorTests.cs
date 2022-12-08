using PrincessProblem;

namespace TestsPrincessProblem;

[TestClass]
public class ContendersGeneratorTests
{
    [TestMethod]
    public void TestAllGeneratedContendersHaveUniqueNames()
    {
        var usedNames = new HashSet<string>();
        var contenders = ContendersGenerator.Generate();
        
        foreach (var contender in contenders)
        {
            Assert.IsFalse(usedNames.Contains(contender.Name));
            usedNames.Add(contender.Name);
        }
    }
}