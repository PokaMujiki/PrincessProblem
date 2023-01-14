using PrincessProblem.Exceptions;

namespace PrincessProblem;

public class Friend
{
    private readonly Dictionary<string, int> _familiarContenders = new();

    public void MarkAsFamiliar(Contender contender)
    {
        _familiarContenders.Add(contender.Name, contender.Rank);
    }

    public Contender ChooseBest(Contender firstContender, Contender secondContender)
    {
        if (!_familiarContenders.ContainsKey(firstContender.Name) ||
            !_familiarContenders.ContainsKey(secondContender.Name))
        {
            throw new UnfamiliarContender();
        }
        
        if (_familiarContenders[firstContender.Name] < _familiarContenders[secondContender.Name])
        {
            return firstContender;
        }

        return secondContender;
    }
}