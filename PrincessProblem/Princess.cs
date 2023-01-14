using Microsoft.Extensions.Hosting;
using PrincessProblem.Utils;

namespace PrincessProblem;

public class Princess : IHostedService
{
    private readonly Hall _hall;
    private readonly Friend _friend;
    
    private Contender? _husband;
    private readonly List<Contender> _rejectedContenders = new();
    
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Princess(IHostApplicationLifetime applicationLifetime, Friend friend, Hall hall)
    {
        _friend = friend;
        _hall = hall;
        _applicationLifetime = applicationLifetime;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            FindHusband();
            Console.WriteLine($"Princess happiness: {GetHappiness()}");

            _applicationLifetime.StopApplication();
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void FindHusband()
    {
        for (var i = 0; i < Constants.ContendersNumber; i++)
        {
            var currentContender = _hall.GetNextContender();

            if (_rejectedContenders.Count < Constants.SkipFirstContendersNumber)
            {
                _rejectedContenders.Add(currentContender);
                continue;
            }

            var currentIsWorseThan =
                _rejectedContenders.Count(contender => contender == _friend.ChooseBest(currentContender, contender));

            if (currentIsWorseThan == 2)
            {
                _husband = currentContender;
                return;
            }
            
            _rejectedContenders.Add(currentContender);
        }
    }
    
    public int GetHappiness()
    {
        return _husband?.Rank switch
        {
            Constants.BestContenderRank => Constants.BestMarriageHappiness,
            Constants.MediumContenderRank => Constants.MediumMarriageHappiness,
            Constants.BadContenderRank => Constants.BadMarriageHappiness,
            null => Constants.NoMarriageHappiness,
            _ => Constants.WorstMarriageHappiness
        };
    }
}