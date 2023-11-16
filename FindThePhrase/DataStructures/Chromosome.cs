namespace FindThePhrase.DataStructures
{
    public class Chromosome
    {
        public string Phrase { get; set; }
        public int Fitness { get; set; }

        public Chromosome(string phrase, int fitness)
        {
            Phrase = phrase;
            Fitness = fitness;
        }
    }
}
