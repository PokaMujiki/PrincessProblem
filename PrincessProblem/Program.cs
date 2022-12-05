namespace PrincessProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            var hall = new Hall();
            var princess = new Princess(hall);
            princess.FindHusband();
            
            Console.WriteLine(hall.GetPrincessHappiness(princess.HusbandName));
        }
    }
}