using FindThePhrase;
using FindThePhrase.Utils;

// Set our initial configuration
const string PHRASE = "La vida no tiene sentido, pero vale la pena vivir, siempre que reconozcas que no tiene sentido";
const string GENE_SET = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!.,";
const int POPULATION_LIMIT = 10000;
const int GENERATION = 10000;

// Instantiates our Genetic class
Genetic genetic = new(PHRASE, GENE_SET, POPULATION_LIMIT);

// Also set our optional environment variables
genetic.Elitsim = 1000;
genetic.Couples = 5000;


// Initialize the population
genetic.InitPopulation();
genetic.UpdateFitnessPopulation();
genetic.SortPopulation();

// passing through generations until finding the optimal solution.
for (int i = 0; i < GENERATION; i++)
{
    genetic.Crossover();
    genetic.Mutation();
    genetic.UpdateFitnessPopulation();
    genetic.SortPopulation();

    Display.ShowLog(genetic.Population, i);
    if (genetic.IsTheFirstChromosomeOptimal()) 
        break;
}


