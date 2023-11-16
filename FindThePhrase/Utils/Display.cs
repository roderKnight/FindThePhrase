using FindThePhrase.DataStructures;

namespace FindThePhrase.Utils
{
    public class Display
    {
        public static void ShowLog(List<Chromosome> population, int index)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"GENERATION Nro {index + 1}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Best chromosome: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{population.First().Phrase} ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"with Fitness: {population.First().Fitness} \n");
        }
    }
}
