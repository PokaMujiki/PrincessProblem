using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using PrincessProblem;
using PrincessProblem.Utils;
using PrincessProblemData;
using PrincessProblemSimulator.Exceptions;

namespace PrincessProblemSimulator;

public class Simulator
{
    private readonly DbContext _context;
    
    public Simulator(DbContext context)
    {
        _context = context;
    }
    
    public void Generate(int attemptsAmount)
    {
        _context.Database.EnsureCreated();
        
        for (var i = 0; i < attemptsAmount; i++)
        {
            var contenders = ContendersGenerator.Generate().Shuffle();
            _context.Set<Attempt>().Add(new Attempt {Contenders = contenders});
        }

        _context.SaveChanges();
    }

    public void Clear()
    {
        _context.Set<Attempt>().RemoveRange(_context.Set<Attempt>());
        _context.Set<Contender>().RemoveRange(_context.Set<Contender>());
        _context.SaveChanges();
    }
    
    public int Reproduce(int attemptId)
    {
        _context.Database.EnsureCreated();

        var attempt = _context.Set<Attempt>()
            .Where(a => a.Id == attemptId)
            .Include(a => a.Contenders);
        
        if (attempt == null || !attempt.Any())
        {
            throw new NoSuchAttempt($"No attempt with this id: {attemptId}");
        }

        var contenders = attempt.First().Contenders;

        var friend = new Friend();
        var hall = new Hall(friend, contenders);
        var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
        var princess = new Princess(_, friend, hall);
        
        princess.FindHusband();

        return princess.GetHappiness();
    }

    public double ReproduceAll()
    {
        _context.Database.EnsureCreated();

        var attempts = _context.Set<Attempt>().Include(a => a.Contenders);
        var attemptsLength = attempts.ToArray().Length;

        if (attemptsLength == 0)
        {
            throw new EmptyAttempts("No generated attempts");
        }
        
        var sum = 0;

        foreach (var attempt in attempts)
        {
            var contenders = attempt.Contenders;

            var friend = new Friend();
            var hall = new Hall(friend, contenders);
            var _ = new ApplicationLifetime(new Logger<ApplicationLifetime>(new LoggerFactory()));
            var princess = new Princess(_, friend, hall);
        
            princess.FindHusband();

            sum += princess.GetHappiness();
        }

        return (double)sum / attemptsLength;
    }
}