using PrincessProblem.Utils;

namespace PrincessProblem;

public class Princess
{
    private readonly Hall _hall;
    private readonly Friend _friend;
    
    private readonly List<Contender> _rejectedContenders = new();
    private Contender? _husband;

    public Princess(Friend friend, Hall hall)
    {
        _friend = friend;
        _hall = hall;
    }
    public void FindHusband()
    {
        for (var i = 0; i < Constants.ContendersNumber; i++)
        {
            var currentContender = _hall.GetNextContender();

            if (_rejectedContenders.Count < Constants.ContendersNumber / 2)
            {
                _rejectedContenders.Add(currentContender);
                continue;
            }

            var currentIsWorseThan =
                _rejectedContenders.Count(contender => contender == _friend.ChooseBest(currentContender, contender));

            if (currentIsWorseThan == 2)
            {
                _husband = currentContender;
                return;
            }
            
            _rejectedContenders.Add(currentContender);
        }
    }
    
    public int GetHappiness()
    {
        return _husband?.Rank switch
        {
            Constants.BestContenderRank => Constants.BestMarriageHappiness,
            Constants.MediumContenderRank => Constants.MediumMarriageHappiness,
            Constants.BadContenderRank => Constants.BadMarriageHappiness,
            null => Constants.NoMarriageHappiness,
            _ => Constants.WorstMarriageHappiness
        };
    }
}