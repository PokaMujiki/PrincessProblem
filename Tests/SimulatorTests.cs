using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using PrincessProblem;
using PrincessProblem.Utils;
using PrincessProblemData;
using PrincessProblemSimulator;
using Constants = PrincessProblemSimulator.Constants;

namespace TestsPrincessProblem;

[TestClass]
public class SimulatorTests
{
    [TestMethod]
    public void TestSimulatorReproduceAllSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseModel(new PrincessProblemContext().Model)
            .Options;
        var context =  new DbContext(options);
        
        context.Set<Attempt>().RemoveRange(context.Set<Attempt>());
        context.Set<Contender>().RemoveRange(context.Set<Contender>());

        double sum = 0;
        for (var i = 0; i < Constants.AttemptsAmount; i++)
        {
            var contenders = ContendersGenerator
                .Generate()
                .Shuffle()
                .Select((c, index) =>
                {
                    c.Id = i * 100 + index + 1;
                    return c;
                })
                .ToList();

            context.Set<Contender>().AddRange(contenders);
            context.Set<Attempt>().Add(new Attempt { Id = i + 1, Contenders = contenders });
            context.SaveChanges();

            var friend = new Friend();
            var hall = new Hall(friend, contenders);
            var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
            var princess = new Princess(_, friend, hall);
            princess.FindHusband();
            sum += princess.GetHappiness();
        }

        var simulator = new Simulator(context);
        
        Assert.AreEqual(sum / Constants.AttemptsAmount, simulator.ReproduceAll());
    }

    [TestMethod]
    public void TestSimulatorReproduceSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseModel(new PrincessProblemContext().Model)
            .Options;
        var context =  new DbContext(options);
        
        context.Set<Attempt>().RemoveRange(context.Set<Attempt>());
        context.Set<Contender>().RemoveRange(context.Set<Contender>());

        var contenders = ContendersGenerator
            .Generate()
            .Shuffle()
            .Select((c, i) => 
            {
                c.Id = i + 1;
                return c;
            })
            .ToList();
        
        context.Set<Contender>().AddRange(contenders);
        context.Set<Attempt>().Add(new Attempt { Id = 1, Contenders = contenders });
        context.SaveChanges();

        var friend = new Friend();
        var hall = new Hall(friend, contenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);
        princess.FindHusband();
        
        var simulator = new Simulator(context);

        Assert.AreEqual(princess.GetHappiness(), simulator.Reproduce(1));
    }
}