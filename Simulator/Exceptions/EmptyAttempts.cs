namespace PrincessProblemSimulator.Exceptions;

public class EmptyAttempts : Exception
{
    public EmptyAttempts(string message) : base(message) { }
}