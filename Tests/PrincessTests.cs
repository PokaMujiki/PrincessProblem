using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using PrincessProblem;
using PrincessProblem.Utils;

namespace TestsPrincessProblem;

[TestClass]
public class PrincessTests
{
    [TestMethod]
    public void TestPrincessSkipFirstContenders()
    {
        var shuffledContenders = ContendersGenerator.Generate().Take(Constants.SkipFirstContendersNumber).ToList();
        var friend = new Friend();
        var hall = new Hall(friend, shuffledContenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);

        try
        {
            princess.FindHusband();
        }
        catch(IndexOutOfRangeException ignored) { }

        Assert.AreEqual(Constants.NoMarriageHappiness, princess.GetHappiness());
    }
    
    [TestMethod]
    public void TestPrincessNoMarriageOnDescendingRanks()
    {
        var contenders = new List<Contender>();
        for (var i = 0; i < Constants.ContendersNumber; i++)
        {
            contenders.Add(new Contender(i.ToString(), i + 1));
        }

        var friend = new Friend();
        var hall = new Hall(friend, contenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);
        
        princess.FindHusband();
        
        Assert.AreEqual(Constants.NoMarriageHappiness, princess.GetHappiness());
    }
    
    [TestMethod]
    public void TestPrincessNoMarriageOnAscendingRanks()
    {
        var contenders = new List<Contender>();
        for (var i = Constants.ContendersNumber; i > 0; i--)
        {
            contenders.Add(new Contender(i.ToString(), i));
        }

        var friend = new Friend();
        var hall = new Hall(friend, contenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);
        
        princess.FindHusband();
        
        Assert.AreEqual(Constants.NoMarriageHappiness, princess.GetHappiness());
    }
    
    [TestMethod]
    public void TestPrincessMarriageOnWorseThanTwoPrevious()
    {
        var contenders = new List<Contender>();

        for (var i = Constants.ContendersNumber; i > 3; i--)
        {
            contenders.Add(new Contender(i.ToString(), i));
        }
        
        contenders.Add(new Contender("1", 1));
        contenders.Add(new Contender("2", 2));
        contenders.Add(new Contender("3", 3));
        
        Console.Write(contenders.Last().Rank);

        var friend = new Friend();
        var hall = new Hall(friend, contenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);
        
        princess.FindHusband();
        
        Assert.AreEqual(Constants.MediumMarriageHappiness, princess.GetHappiness());
    }
}