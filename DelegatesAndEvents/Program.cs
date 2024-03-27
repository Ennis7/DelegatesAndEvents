using System;

namespace delegatesAndEvents
{
    public delegate void Notify(int n);  // delegate

    public class Race
    {
        public event Notify Results; // event

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {

                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }

            }

            TheWinner(champ);
        }
        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            // invoke the delegate event object and pass champ to the method
            Results?.Invoke(champ);

        }
    }
    class Program
    {
        public static void Main()
        {
            // create a class object
            Race round1 = new Race();


            // register with the footRace event
            round1.Results += footRace;
            // trigger the event
            round1.Racing(80, 100);


            // register with the carRace event
            round1.Results -= footRace;
            round1.Results += carRace;
            //trigger the event
            round1.Racing(20,100);
            round1.Results -= carRace;
            // register a bike race event using a lambda expression
            //round1.Results -= carRace();
            round1.Results += (winner) => Console.WriteLine($"Bike number {winner} is the winner.");

            // trigger the event
            round1.Racing(30, 100);
            Console.ReadKey();
        }

        // event handlers
        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
            Console.WriteLine();
            Console.ReadKey();
        }
        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}