namespace PrincessProblem;

public class Princess
{
    private List<String> _rejectedContendersNames;
    private Hall _hall;
    public string? HusbandName { get; private set; }

    public Princess(Hall hall)
    {
        _rejectedContendersNames = new List<String>();
        _hall = hall;
    }

    public void FindHusband()
    {
        for (int i = 0; i < 100; i++)
        {
            _hall.CallNextContender();
            var currentContenderName = _hall.GetCurrentContenderName();

            int currentBetterThan = 0;

            foreach (var rejectedContenderName in _rejectedContendersNames)
            {
                var bestName = _hall.Friend.ChooseBest(rejectedContenderName, currentContenderName);
                if (bestName == currentContenderName)
                {
                    currentBetterThan++;
                }
            }

            if (currentBetterThan == _rejectedContendersNames.Count - 1 && _rejectedContendersNames.Count > 36)
            {
                HusbandName = currentContenderName;
                return;
            }
            
            _rejectedContendersNames.Add(currentContenderName);
        }
    }
}