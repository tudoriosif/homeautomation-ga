# Home Automation Genetic Algorithm Project

## Overview
This project is a demonstration of using Genetic Algorithms (GA) for optimizing a home automation system. The goal is to balance energy efficiency and user comfort by controlling 3 lights, 2 thermostats, and 2 shades. The project utilizes the GeneticSharp library, a powerful and versatile .NET library for GA.

## Features
- **Chromosome Representation**: A unique representation of home automation settings, where each chromosome consists of 3 lights, 2 thermostats, and 2 shades.
- **Custom Fitness Function**: Evaluates the energy consumption and comfort level to find the optimal balance.
- **Genetic Operators**:
  - **Order-Based Crossover**: Ensures a meaningful crossover that respects the chromosome structure.
  - **Insertion Mutation**: Introduces variability in the population while maintaining the integrity of each device type.

## Getting Started
To run this project, clone the repository and build it in an environment that supports .NET development, such as Visual Studio.

### Prerequisites
- .NET compatible environment
- GeneticSharp library

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/tudoriosif/homeautomation-ga
2. Open the solution in your .NET development environment.
3. Restore NuGet packages to resolve dependencies, especially GeneticSharp.

## Usage
The main focus of the project is the HomeAutomationGA class, which sets up and runs the genetic algorithm. To see the GA in action, simply run the project. The console output will display the final result of GA computation, showing how the best fitness value evolved.

## Customization
You can modify the parameters of the GA, such as population size, mutation rate, etc., in the HomeAutomationGA class. Additionally, the fitness function can be adjusted in the HomeAutomationFitness class to cater to different optimization goals or to reflect different home automation setups.

## Contributing
Contributions to this project are welcome. Please open an issue or submit a pull request if you have suggestions or improvements.

## License
Distributed under the MIT License. See LICENSE for more information.

## Acknowledgments
GeneticSharp Library

The .NET community