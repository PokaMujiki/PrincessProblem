namespace PrincessProblem;

public class Friend
{
    private Dictionary<string, int> _contenders;
    
    public Friend(List<Contender> contenders)
    {
        _contenders = new Dictionary<string, int>();

        foreach (var contender in contenders)
        {
            _contenders.Add(contender.Name, contender.Rank);
        }
    }

    public string ChooseBest(string firstContenderName, string secondContenderName)
    {
        if (_contenders[firstContenderName] < _contenders[secondContenderName])
        {
            return firstContenderName;
        }

        return secondContenderName;
    }
}