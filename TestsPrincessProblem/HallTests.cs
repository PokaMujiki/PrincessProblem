using PrincessProblem;
using PrincessProblem.Exceptions;
using PrincessProblem.Utils;

namespace TestsPrincessProblem;

[TestClass]
public class HallTests
{
    [TestMethod]
    public void HallCallingNextContenderIsCorrect()
    {
        var shuffledContenders = ContendersGenerator.Generate().Shuffle();
        var friend = new Friend();
        var hall = new Hall(friend, shuffledContenders);
        
        foreach (var contender in shuffledContenders)
        {
            Assert.AreEqual(contender, hall.GetNextContender());   
        }
        
        // no more contenders
        Assert.ThrowsException<NoContenderException>(() => hall.GetNextContender());
    }
}