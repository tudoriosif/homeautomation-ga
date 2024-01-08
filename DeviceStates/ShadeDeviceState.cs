namespace HomeAutomationGA
{
    public class ShadeDeviceState : IDeviceState
    {
        public ShadeState State { get; set; }

        public ShadeDeviceState(ShadeState shadeState)
        {
            State = shadeState;
        }
    }
}
