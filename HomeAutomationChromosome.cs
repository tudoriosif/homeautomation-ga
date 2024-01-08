using GeneticSharp;

namespace HomeAutomationGA
{
    public class HomeAutomationChromosome : ChromosomeBase
    {
        public HomeAutomationChromosome(int numberOfDevices) : base(numberOfDevices) 
        {
            CreateGenes();
        }

        // First 3 CHs are represeting Lights
        public static bool IsLightDevice(int geneIndex) => geneIndex < HomeAutomationConfig.Instance.NumberOfLights;

        // Following 2 CHs are represeting Thermostats
        public static bool IsThermostatDevice(int geneIndex) => geneIndex >= HomeAutomationConfig.Instance.NumberOfLights
                                                         && geneIndex < HomeAutomationConfig.Instance.NumberOfLights + HomeAutomationConfig.Instance.NumberOfThermostats;

        // Last 2 CHs are represeting Shades
        public static bool IsShadesDevice(int geneIndex) => geneIndex >= HomeAutomationConfig.Instance.NumberOfLights + HomeAutomationConfig.Instance.NumberOfThermostats
                                                         && geneIndex < HomeAutomationConfig.Instance.NumberOfDevices;

        public override Gene GenerateGene(int geneIndex)
        {
            if (IsLightDevice(geneIndex))
            {
                // For lights, just on or off
                return new Gene(new LightDeviceState(RandomizationProvider.Current.GetInt(0, 2) == 1));
            }
            else if (IsThermostatDevice(geneIndex))
            {
                // For thermostats, a range of temperatures
                return new Gene(new ThermostatDeviceState(RandomizationProvider.Current.GetDouble(HomeAutomationConfig.Instance.MinTemperature, HomeAutomationConfig.Instance.MaxTemperature + 0.1)));
            }
            else if (IsShadesDevice(geneIndex))
            {
                // For shades, any value from ShadeState (0, 1, 2)
                return new Gene(new ShadeDeviceState((ShadeState)RandomizationProvider.Current.GetInt(0, 3)));
            }

            // Default case
            return new Gene(new LightDeviceState(RandomizationProvider.Current.GetInt(0, 2) == 1));
        }

        public override IChromosome CreateNew()
        {
            return new HomeAutomationChromosome(Length);
        }

    }
}
