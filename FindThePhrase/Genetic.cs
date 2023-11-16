using FindThePhrase.DataStructures;
using FindThePhrase.Utils;

namespace FindThePhrase
{
    public class Genetic
    {

        #region Environment Variables
        /// <summary>
        /// The Gene Set as a string (or array of char)
        /// </summary>
        public string GeneSet { get; set; }

        /// <summary>
        /// The target or the right solution
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The maximun limit of the population
        /// </summary>
        public int PopulationLimit { get; set; }

        /// <summary>
        /// The maximun length of a Chromosome
        /// </summary>
        public int ChromosomeLength { get; set; }

        /// <summary>
        /// The population
        /// </summary>
        public List<Chromosome> Population { get; set; } = new List<Chromosome>();
        #endregion


        #region Optional Variables
        /// <summary>
        /// The number of allowed couples
        /// </summary>
        public int Couples { get; set; } = 0;

        /// <summary>
        /// The number of Elitism, chromosomes that wont be mutated
        /// </summary>
        public int Elitsim { get; set; } = 0;

        /// <summary>
        /// The percentage of the mutation scope
        /// </summary>
        public int MutationScope { get; set; } = 50;
        #endregion


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="target">The right answer</param>
        /// <param name="geneSet">The Gene Set</param>
        /// <param name="populationLimit">The maximun limit of the population</param>
        public Genetic(string target, string geneSet, int populationLimit)
        {
            Target = target;
            GeneSet = geneSet;
            PopulationLimit = populationLimit;
            ChromosomeLength = Target.Length;
        }

        /// <summary>
        /// Initialize the population using the Gene Set to create chromosomes with random genes
        /// </summary>
        public void InitPopulation()
        {
            Population.Clear();
            for (int i = 0; i < PopulationLimit; i++)
            {
                string individualGenes = GetSampleFromGenes();
                Chromosome generatedIndividual = new(individualGenes, 0);
                Population.Add(generatedIndividual);
            }
        }

        /// <summary>
        /// Updates the Fitness value of the whole population
        /// </summary>
        public void UpdateFitnessPopulation()
        {
            foreach (Chromosome individual in Population)
            {
                individual.Fitness = GetFitness(individual.Phrase);
            }
        }

        /// <summary>
        /// Sorts the population in ascending order
        /// </summary>
        public void SortPopulation()
        {
            Population.Sort((ind1, ind2) => ind2.Fitness.CompareTo(ind1.Fitness));
        }


        /// <summary>
        /// Perform the crossover stage.
        /// Randomly choose two chromosomes and cross them, from them two new chromosomes (offsprings) are born and replace other chromosomes with lower fitness
        /// </summary>
        public void Crossover()
        {
            if (Couples == 0)
                Couples = 20;

            int populationDelimiter = PopulationLimit / 2;
            int genesDelimiter = ChromosomeLength / 2;
            Random rnd = new();

            for (int i = 0; i < Couples; i++)
            {
                // randomly select the parents
                Chromosome parent1 = Population.ElementAt(rnd.Next(0, populationDelimiter - 1));
                Chromosome parent2 = Population.ElementAt(rnd.Next(0, populationDelimiter - 1));

                // creates a new offspring using the union of the parent #1 and parent #2 genes respectivaly 
                string phrase1 = parent1.Phrase.Substring(0, genesDelimiter)
                    + parent2.Phrase.Substring(genesDelimiter);
                Chromosome offspring1 = new(phrase1, 0);
                Population[rnd.Next(populationDelimiter, PopulationLimit - 1)] = offspring1;

                // creates a new offspring using the union of the parent #2 and parent #1 genes respectivaly 
                string phrase2 = parent2.Phrase.Substring(0, genesDelimiter)
                    + parent1.Phrase.Substring(genesDelimiter);
                Chromosome offspring2 = new(phrase2, 0);
                Population[rnd.Next(populationDelimiter, PopulationLimit - 1)] = offspring2;
            }
        }

        /// <summary>
        /// Perform the mutation stage to the whole population
        /// </summary>
        public void Mutation()
        {
            if (Elitsim == 0)
                Elitsim = 10;

            Random rnd = new();
            int mutationCount = PopulationLimit * MutationScope / 100;
            for (int i = 0; i < mutationCount; i++)
            {
                // do not mutate chromosomes with higher fitness
                int index = rnd.Next(Elitsim, PopulationLimit - 1);

                Chromosome mutatedIndividual = Population.ElementAt(index);
                char[] childGenes = mutatedIndividual.Phrase.ToArray();

                // get 2 random genes: the new gene and one alternate
                List<char> randomGenes = rnd.MultiSample(GeneSet.ToList(), 2);
                char newGene = randomGenes[0];
                char alternate = randomGenes[1];

                // changes the genes on the chromosome, if the new gene is the same, use the alternative
                int geneIndex = rnd.Next(ChromosomeLength);
                childGenes[geneIndex] = newGene == childGenes[geneIndex] ? alternate : newGene;
                mutatedIndividual.Phrase = new string(childGenes);
            }
        }


        #region HelperMethods
        /// <summary>
        /// Returns <c>True</c> if the first chromosome has the exact same Fitness as the target
        /// </summary>
        /// <returns></returns>
        public bool IsTheFirstChromosomeOptimal()
        {
            bool isOptimal = Population.First().Fitness == Target.Length;

            if (isOptimal)
                Console.WriteLine("Optimal solution found!");

            return isOptimal;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a Chromosome using random genes
        /// </summary>
        /// <returns></returns>
        private string GetSampleFromGenes()
        {
            string randomSample = string.Empty;
            Random rnd = new();
            string localGeneSet = GeneSet;

            while (randomSample.Length < ChromosomeLength)
            {
                var index = rnd.Next(localGeneSet.Length);
                string selectedLetter = localGeneSet.Substring(index, 1);
                randomSample += selectedLetter;
            }

            return randomSample;
        }

        /// <summary>
        /// Get the Fitnes values from the chromosome itself
        /// </summary>
        /// <param name="individualPhrase">The chromosome phrase</param>
        /// <returns></returns>
        private int GetFitness(string individualPhrase)
        {
            int fitness = 0;
            for (int i = 0; i < ChromosomeLength && i < Target.Length; i++)
            {
                if (Target[i] == individualPhrase[i])
                {
                    fitness++;
                }
            }
            return fitness;
        }

        #endregion


    }
}
