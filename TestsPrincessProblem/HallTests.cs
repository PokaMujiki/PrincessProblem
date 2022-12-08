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
        var hall = new Hall(shuffledContenders);
        
        // no contenders call
        Assert.ThrowsException<NoContenderException>(() => hall.GetCurrentContenderName());

        foreach (var contender in shuffledContenders)
        {
            hall.CallNextContender();
            Assert.AreEqual(contender.Name, hall.GetCurrentContenderName());   
        }
        
        // no more contenders
        hall.CallNextContender();
        Assert.ThrowsException<NoContenderException>(() => hall.GetCurrentContenderName());
    }
}