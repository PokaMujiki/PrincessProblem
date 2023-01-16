using NDesk.Options;
using PrincessProblemData;
using PrincessProblemSimulator.Exceptions;

namespace PrincessProblemSimulator;

public static class Program
{
    public static void Main(string[] args)
    {
        var simulator = new Simulator(new PrincessProblemContext());
        var options = new OptionSet 
        {
            {
                "r|reproduce=", "reproduce result of attempt with this id", 
                (int a) =>
                {
                    try
                    {
                        Console.WriteLine("Princess happiness:  " + simulator.Reproduce(a));
                    }
                    catch (NoSuchAttempt e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            },
            {
                "a|average", "average princess happiness on all attempts", 
                _ =>
                {
                    try
                    {
                        Console.WriteLine("Average princess happiness on all attempts: " + simulator.ReproduceAll());
                    }
                    catch (EmptyAttempts e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            },
            {
                "g|generate", $"generate {Constants.AttemptsAmount} attempts",
                _ =>
                {
                    simulator.Generate(Constants.AttemptsAmount);
                    Console.WriteLine($"Generated {Constants.AttemptsAmount} attempts");
                }
            },
            {
                "c|clear", "clear all attempts", _ =>
                {
                    simulator.Clear();
                    Console.WriteLine("Cleared all attempts");
                }
            }
        };

        if (args.Length == 0)
        {
            options.WriteOptionDescriptions(Console.Out);
        }

        options.Parse(args);
    }
}