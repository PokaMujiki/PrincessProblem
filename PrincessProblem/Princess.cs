using Microsoft.Extensions.Hosting;

namespace PrincessProblem;

public class Princess : IHostedService
{
    private readonly List<string> _rejectedContendersNames = new();
    private readonly Hall _hall;
    private string? HusbandName { get; set; }
    private const int ContendersNumber = 100;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Princess(IHostApplicationLifetime applicationLifetime, Hall hall)
    {
        _hall = hall;
        _applicationLifetime = applicationLifetime;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            FindHusband();
            Console.WriteLine($"Princess happiness: {_hall.GetPrincessHappiness(HusbandName)}");

            _applicationLifetime.StopApplication();
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void FindHusband()
    {
        for (var i = 0; i < ContendersNumber; i++)
        {
            _hall.CallNextContender();
            var currentContenderName = _hall.GetCurrentContenderName();

            if (_rejectedContendersNames.Count <= ContendersNumber / 2)
            {
                _rejectedContendersNames.Add(currentContenderName);
                continue;
            }

            var currentIsBetterThan = _rejectedContendersNames
                .Select(name => _hall.Friend.ChooseBest(name, currentContenderName))
                .Count(bestName => bestName == currentContenderName);

            if (currentIsBetterThan == _rejectedContendersNames.Count - 2)
            {
                HusbandName = currentContenderName;
                return;
            }
            
            _rejectedContendersNames.Add(currentContenderName);
        }
    }
}