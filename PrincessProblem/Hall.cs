namespace PrincessProblem;

public class Hall
{
    private List<Contender> _contenders;
    private int _currentContenderIndex;

    public Friend Friend;

    public Hall()
    {
        _contenders = ContendersGenerator.Generate().Shuffle();
        _currentContenderIndex = -1;

        Friend = new Friend(_contenders);
    }

    public String GetCurrentContenderName()
    {
        return _contenders[_currentContenderIndex].Name;
    }

    public void CallNextContender()
    {
        _currentContenderIndex++;
    }

    public int GetPrincessHappiness(string? husbandName)
    {
        var husband = _contenders.Find(contender => contender.Name == husbandName);

        if (husband == null)
        {
            return 10;
        }
        
        switch (husband.Rank)
        {
            case 1:
                return 20;
            case 3:
                return 50;
            case 5:
                return 100;
            default:
                return 0;
        }
    }
}