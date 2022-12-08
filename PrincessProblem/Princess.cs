using Microsoft.Extensions.Hosting;

namespace PrincessProblem;

public class Princess : IHostedService
{
    private readonly List<string> _rejectedContendersNames = new();
    private readonly Hall _hall;
    public string? HusbandName { get; private set; }
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

    public void FindHusband()
    {
        for (var i = 0; i < Utils.Constants.ContendersNumber; i++)
        {
            _hall.CallNextContender();
            var currentContenderName = _hall.GetCurrentContenderName();

            if (_rejectedContendersNames.Count < Utils.Constants.ContendersNumber / 2)
            {
                _rejectedContendersNames.Add(currentContenderName);
                continue;
            }

            var currentIsWorseThan =
                _rejectedContendersNames.Count(name => name == _hall.Friend.ChooseBest(currentContenderName, name));

            if (currentIsWorseThan == 2)
            {
                HusbandName = currentContenderName;
                return;
            }
            
            _rejectedContendersNames.Add(currentContenderName);
        }
    }
}