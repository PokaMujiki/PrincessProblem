namespace PrincessProblem;

public static class Utils
{
    public static List<T> Shuffle<T>(this List<T> list)
    {
        int seed = (int)DateTime.Now.Ticks;
        Console.WriteLine($"Seed for shuffle is: {seed}");
        Random random = new Random(seed);
        int n = list.Count;
        
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            (list[n], list[k]) = (list[k], list[n]);
        }

        return list;
    }
}