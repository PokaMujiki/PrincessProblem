namespace PrincessProblem;

public class Contender
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int Rank { get; init; }
    
    public Contender(string name, int rank)
    {
        Name = name;
        Rank = rank;
    }
}