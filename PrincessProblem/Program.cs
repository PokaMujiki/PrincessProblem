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
                var hall = new Hall();
                var princess = new Princess(hall);
                princess.FindHusband();

                Console.WriteLine($"Princess happiness: {hall.GetPrincessHappiness(princess.HusbandName)}");
                acc += hall.GetPrincessHappiness(princess.HusbandName);
            }

            var mean = (double)acc / ExperimentsAmount;
            Console.WriteLine($"Mean happiness: {mean}");
        }
    }
}