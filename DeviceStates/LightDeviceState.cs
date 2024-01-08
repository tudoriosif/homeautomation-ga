namespace HomeAutomationGA
{
    public class LightDeviceState : IDeviceState
    {
        public bool IsOn { get; set; }

        public LightDeviceState(bool isOn)
        {
            IsOn = isOn;
        }
    }
}
