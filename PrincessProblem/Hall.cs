using PrincessProblem.Exceptions;
using PrincessProblem.Utils;

namespace PrincessProblem;

public class Hall
{
    private readonly List<Contender> _contenders;
    private int _currentContenderIndex;

    public readonly Friend Friend;

    public Hall(List<Contender>? shuffledContendersList = null)
    {
        shuffledContendersList ??= ContendersGenerator.Generate().Shuffle();
        _contenders = shuffledContendersList;
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

        var currentContenderName = _contenders[_currentContenderIndex].Name;
        Friend.MarkAsFamiliar(currentContenderName);
        
        return currentContenderName;
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