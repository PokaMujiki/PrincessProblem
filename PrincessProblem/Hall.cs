using PrincessProblem.Exceptions;
using PrincessProblem.Utils;

namespace PrincessProblem;

public class Hall
{
    private readonly Contender[] _contenders;
    private int _currentContenderIndex;

    private readonly Friend _friend;

    public Hall(Friend friend, List<Contender>? shuffledContendersList = null)
    {
        _friend = friend;
        
        shuffledContendersList ??= ContendersGenerator.Generate().Shuffle();
        _contenders = shuffledContendersList.ToArray();
    }

    public Contender GetNextContender()
    {
        if (_currentContenderIndex >= Constants.ContendersNumber)
        {
            throw new NoContenderException("No more contenders");
        }
        
        var currentContender = _contenders[_currentContenderIndex];
        _currentContenderIndex++;
        _friend.MarkAsFamiliar(currentContender);
        
        return currentContender;
    }
}