using GeneticSharp;

namespace HomeAutomationGA
{
    public class HomeAutomationFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var genes = chromosome.GetGenes();
            double totalFitness = 0;

            foreach (var gene in genes)
            {
                if (gene.Value is LightDeviceState lightDeviceState)
                {
                    totalFitness += EvaluateLight(lightDeviceState);
                }
                else if (gene.Value is ThermostatDeviceState thermostatDeviceState)
                {
                    totalFitness += EvaluateThermostat(thermostatDeviceState);
                }
                else if (gene.Value is ShadeDeviceState shadeState)
                {
                    totalFitness += EvaluateShade(shadeState);
                }
            }

            return totalFitness; // The GA will aim to minimize this value
        }

        private double EvaluateLight(LightDeviceState lightState)
        {
            const double LightEnergyUsage = 10; // Energy used by a light when it's on
            const double LightComfortValue = 5; // Comfort value provided by a light when it's on
            const int NightStartHour = 18; // 6 PM start for "night"
            const int NightEndHour = 6;   // 6 AM end for "night"
            int CurrentHour = DateTime.Now.Hour; // Current hour of the day

            double energyConsumption = 0;
            double comfortScore = 0;

            if (lightState.IsOn)
            {
                // If light is on then we must increase energyConsumption
                energyConsumption += LightEnergyUsage;

                // We must increase comfort only when it's night and light it's on
                if (CurrentHour >= NightStartHour || CurrentHour <= NightEndHour)
                {
                    comfortScore += LightComfortValue;
                }
            }

            return energyConsumption - comfortScore; // Minimizing energy consumption and maximizing comfort
        }

        private double EvaluateThermostat(ThermostatDeviceState thermostatState)
        {
            const double BaseThermostatEnergyUsage = 12; // Base energy usage
            const double ThermostatEnergyUsageFactor = 0.5; // Factor for calculating energy based on temp difference
            const int OptimalTemperature = 21; // Optimal temperature for comfort
            const int ComfortRange = 3; // Range around optimal temperature considered comfortable
            const int HighComfortScore = 7;
            const int LowComfortScore = 3;

            double energyConsumption = 0;
            double comfortScore = 0;
            
            double temperatureDifference = Math.Abs(thermostatState.Temperature - OptimalTemperature);
            energyConsumption += BaseThermostatEnergyUsage + temperatureDifference * ThermostatEnergyUsageFactor;

            // Comfort is highest near the optimal temperature
            comfortScore += (Math.Abs(thermostatState.Temperature - OptimalTemperature) <= ComfortRange)
                ? HighComfortScore : LowComfortScore;
            

            return energyConsumption - comfortScore; // Minimizing energy consumption and maximizing comfort
        }

        private double EvaluateShade(ShadeDeviceState shadeState)
        {
            const double ShadeEnergyImpactOpen = -5;   // Energy saved (or reduced usage) when shades are open
            const double ShadeEnergyImpactClosed = 3;   // Energy usage increase or decrease when shades are closed
            const double ShadeComfortOpen = 7;         // Comfort value when shades are open
            const double ShadeComfortClosed = 4;        // Comfort value when shades are closed

            double energyConsumption = 0;
            double comfortScore = 0;

            switch (shadeState.State)
            {
                case ShadeState.Open:
                    energyConsumption += ShadeEnergyImpactOpen;
                    comfortScore += ShadeComfortOpen;
                    break;
                case ShadeState.HalfOpen:
                    // Assuming half the impact of fully open/closed
                    energyConsumption += ShadeEnergyImpactOpen / 2;
                    comfortScore += (ShadeComfortOpen + ShadeComfortClosed) / 2;
                    break;
                case ShadeState.Closed:
                    energyConsumption += ShadeEnergyImpactClosed;
                    comfortScore += ShadeComfortClosed;
                    break;
            }

            return energyConsumption - comfortScore; // Minimizing energy consumption and maximizing comfort
        }

    }
}
