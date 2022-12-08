using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using PrincessProblem;
using PrincessProblem.Utils;

namespace TestsPrincessProblem;

[TestClass]
public class PrincessTests
{
    [TestMethod]
    public void TestPrincessFollowsStrategy()
    {
        for (var i = 0; i < 10000; i++)
        {
            var shuffledContenders = ContendersGenerator.Generate().Shuffle();
            var hall = new Hall(shuffledContenders);
            var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));

            var princess = new Princess(_, hall);
            princess.FindHusband();

            var rejectedContenders = new List<Contender>();
            Contender? husband = null;

            // skip first half, find first who is worse than only 2 rejected
            foreach (var contender in shuffledContenders)
            {
                if (rejectedContenders.Count < Constants.ContendersNumber / 2)
                {
                    rejectedContenders.Add(contender);
                    continue;
                }

                if (rejectedContenders.Count(rejectedContender => rejectedContender.Rank < contender.Rank) == 2)
                {
                    husband = contender;
                    break;
                }

                rejectedContenders.Add(contender);
            }

            Assert.AreEqual(husband?.Name, princess.HusbandName);
        }
    }
}