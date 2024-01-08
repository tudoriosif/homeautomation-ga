using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationGA
{
    public class HomeAutomationGA
    {
        private static void DisplayGeneInformation(HomeAutomationChromosome chromosome)
        {
            // Loop through each gene and display its value
            foreach (var gene in chromosome.GetGenes())
            {
                switch(gene.Value)
                {
                    case ThermostatDeviceState thermostatDeviceState:
                        Console.WriteLine($"Gene Type Thermostat (Celsius): {thermostatDeviceState.Temperature}");
                        break;
                    case LightDeviceState lightDeviceState:
                        Console.WriteLine($"Gene Type Light (is On?): {lightDeviceState.IsOn}");
                        break;
                    case ShadeDeviceState shadeDeviceState:
                        Console.WriteLine($"Gene Type Shade (Open/Half-Open/Closed): {shadeDeviceState.State}");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            // Initialize GA components
            var selection = new EliteSelection(); // Select best ch to be parents
            var crossover = new UniformCrossover(); // Uniformly mmixes genes of pareents ch
            var mutation = new DisplacementMutation(); // A substring is randomly selected from chromosome, is removed, then replaced at a randomly selected position

            var chromosome = new HomeAutomationChromosome(HomeAutomationConfig.Instance.NumberOfDevices);
            var population = new Population(50, 100, chromosome);
            var fitness = new HomeAutomationFitness();

            // Setup GA
            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            
            // Run 100 times
            ga.Termination = new GenerationNumberTermination(100);

            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as HomeAutomationChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                // Display generation number and best fitness of this generation
                Console.WriteLine($"Generation {ga.GenerationsNumber} - Best Fitness: {bestFitness}");

                // Optionally, display detailed gene information
                DisplayGeneInformation(bestChromosome);
            };


            ga.TerminationReached += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as HomeAutomationChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                Console.WriteLine("GA completed!");
                Console.WriteLine($"Best Fitness: {bestFitness}");

                // Display best gene information
                DisplayGeneInformation(bestChromosome);
            };

            ga.Start();
        }
    }
}
