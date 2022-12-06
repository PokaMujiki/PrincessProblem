using PrincessProblem.Exceptions;

namespace PrincessProblem;

public class Hall
{
    private readonly List<Contender> _contenders;
    private int _currentContenderIndex;

    public readonly Friend Friend;

    public Hall()
    {
        _contenders = ContendersGenerator.Generate().Shuffle();
        _currentContenderIndex = -1;

        Friend = new Friend(_contenders);
    }

    public string GetCurrentContenderName()
    {
        if (_currentContenderIndex == -1)
        {
            throw new NoContenderException("Call contender first before trying to get his name");
        }

        if (_currentContenderIndex >= _contenders.Count)
        {
            throw new NoContenderException("No more contenders in hall");
        }
        
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

        return husband.Rank switch
        {
            1 => 20,
            3 => 50,
            5 => 100,
            _ => 0
        };
    }
}