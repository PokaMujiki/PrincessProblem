namespace PrincessProblem;

public class Princess
{
    private readonly List<string> _rejectedContendersNames = new();
    private readonly Hall _hall;
    public string? HusbandName { get; private set; }

    public Princess(Hall hall)
    {
        _hall = hall;
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