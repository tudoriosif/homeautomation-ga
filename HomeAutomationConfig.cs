using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationGA
{
    public class HomeAutomationConfig
    {
        private static HomeAutomationConfig _instance;

        public static HomeAutomationConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HomeAutomationConfig();
                return _instance;
            }
        }

        public int NumberOfLights { get; private set; } = 3;
        public int NumberOfThermostats { get; private set; } = 2;
        public int NumberOfShades { get; private set; } = 2;
        public double MinTemperature { get; private set; } = 16.5;
        public double MaxTemperature { get; private set; } = 24.5;

        public int NumberOfDevices
        {
            get
            {
                return NumberOfLights + NumberOfThermostats + NumberOfShades;
            }
        } 

        private HomeAutomationConfig() { }
    }
}
