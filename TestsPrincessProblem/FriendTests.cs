using PrincessProblem;
using PrincessProblem.Exceptions;

namespace TestsPrincessProblem;

[TestClass]
public class FriendTests
{
    [TestMethod]
    public void TestContendersComparisonIsCorrect()
    {
        var contenders = ContendersGenerator.Generate();
        var friend = new Friend(contenders);

        foreach (var contender in contenders)
        {
            friend.MarkAsFamiliar(contender.Name);
        }

        foreach (var firstContender in contenders)
        {
            foreach (var secondContender in contenders)
            {
                Assert.AreEqual(firstContender.Rank < secondContender.Rank ? firstContender.Name : secondContender.Name, 
                    friend.ChooseBest(firstContender.Name, secondContender.Name));
            }
        }
    }

    [TestMethod]
    public void TestUnfamiliarContenderCompared()
    {
        var contenders = ContendersGenerator.Generate();
        var friend = new Friend(contenders);

        // both contenders are unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[0].Name, contenders[1].Name));
        
        friend.MarkAsFamiliar(contenders[0].Name);
        
        // second contender is unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[0].Name, contenders[1].Name));
        
        // first contender is unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[1].Name, contenders[0].Name));
    }

    [TestMethod]
    public void TestComparingFamiliarContendersDoesNotThrowException()
    { 
        var contenders = ContendersGenerator.Generate();
        var friend = new Friend(contenders);
        
        friend.MarkAsFamiliar(contenders[0].Name);
        friend.MarkAsFamiliar(contenders[1].Name);

        try
        {
            friend.ChooseBest(contenders[0].Name, contenders[1].Name);
            friend.ChooseBest(contenders[1].Name, contenders[0].Name);
        }
        catch (UnfamiliarContender)
        {
            Assert.Fail("Contenders are familiar, but friend still throws exception");
        }
    }
}