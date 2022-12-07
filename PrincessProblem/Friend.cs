using PrincessProblem.Exceptions;

namespace PrincessProblem;

public class Friend
{
    private readonly Dictionary<string, int> _contenders = new();
    private readonly HashSet<string> _familiarContendersNames = new();

    public Friend(List<Contender> contenders)
    {
        foreach (var contender in contenders)
        {
            _contenders.Add(contender.Name, contender.Rank);
        }
    }

    public void MarkAsFamiliar(string contenderName)
    {
        _familiarContendersNames.Add(contenderName);
    }

    public string ChooseBest(string firstContenderName, string secondContenderName)
    {
        if (!_familiarContendersNames.Contains(firstContenderName) ||
            !_familiarContendersNames.Contains(secondContenderName))
        {
            throw new UnfamiliarContender();
        }
        
        if (_contenders[firstContenderName] < _contenders[secondContenderName])
        {
            return firstContenderName;
        }

        return secondContenderName;
    }
}