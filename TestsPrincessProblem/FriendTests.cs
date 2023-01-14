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
        var friend = new Friend();

        foreach (var contender in contenders)
        {
            friend.MarkAsFamiliar(contender);
        }

        foreach (var firstContender in contenders)
        {
            foreach (var secondContender in contenders)
            {
                Assert.AreEqual(firstContender.Rank < secondContender.Rank ? firstContender : secondContender, 
                    friend.ChooseBest(firstContender, secondContender));
            }
        }
    }

    [TestMethod]
    public void TestUnfamiliarContenderCompared()
    {
        var contenders = ContendersGenerator.Generate();
        var friend = new Friend();

        // both contenders are unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[0], contenders[1]));
        
        friend.MarkAsFamiliar(contenders[0]);
        
        // second contender is unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[0], contenders[1]));
        
        // first contender is unfamiliar
        Assert.ThrowsException<UnfamiliarContender>(() => friend.ChooseBest(contenders[1], contenders[0]));
    }

    [TestMethod]
    public void TestComparingFamiliarContendersDoesNotThrowException()
    { 
        var contenders = ContendersGenerator.Generate();
        var friend = new Friend();
        
        friend.MarkAsFamiliar(contenders[0]);
        friend.MarkAsFamiliar(contenders[1]);

        try
        {
            friend.ChooseBest(contenders[0], contenders[1]);
            friend.ChooseBest(contenders[1], contenders[0]);
        }
        catch (UnfamiliarContender)
        {
            Assert.Fail("Contenders are familiar, but friend still throws exception");
        }
    }
}