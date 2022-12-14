namespace PrincessProblem
{
    public static class Program
    {
        private const int ExperimentsAmount = 1000;

        public static void Main(string[] args)
        {
            var acc = 0;
            for (var i = 0; i < ExperimentsAmount; i++)
            {
                var friend = new Friend();
                var hall = new Hall(friend);
                var princess = new Princess(friend, hall);
                princess.FindHusband();

                Console.WriteLine($"Princess happiness: {princess.GetHappiness()}");
                acc += princess.GetHappiness();
            }

            var mean = (double)acc / ExperimentsAmount;
            Console.WriteLine($"Mean happiness: {mean}");
        }
    }
}