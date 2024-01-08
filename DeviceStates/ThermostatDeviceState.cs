namespace HomeAutomationGA
{
    public class ThermostatDeviceState : IDeviceState
    {
        public double Temperature { get; set; }

        public ThermostatDeviceState(double temperature)
        {
            Temperature = temperature;
        }
    }

}
