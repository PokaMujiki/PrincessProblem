namespace PrincessProblem;

public class Princess
{
    private readonly List<string> _rejectedContendersNames;
    private readonly Hall _hall;
    public string? HusbandName { get; private set; }
    private const int ContendersNumber = 100;

    public Princess(Hall hall)
    {
        _rejectedContendersNames = new List<string>();
        _hall = hall;
    }

    public void FindHusband()
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