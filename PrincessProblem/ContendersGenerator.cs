namespace PrincessProblem;

public class ContendersGenerator
{
    public static List<Contender> Generate()
    {
        string[] firstNames = { "John", "David", "Michael", "James", "William",
            "Robert", "Joseph", "Charles", "Thomas", "Christopher" };
        string[] secondNames = { "Smith", "Johnson", "Williams", "Jones", "Brown",
            "Davis", "Miller", "Wilson", "Moore", "Taylor" };

        var fullNames = from name in firstNames
            from surname in secondNames
            select name + " " + surname;

        return fullNames.Select((name, index) => new Contender(name, index + 1)).ToList();
    }
}