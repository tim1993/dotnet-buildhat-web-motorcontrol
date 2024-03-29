using Iot.Device.BuildHat.Models;

namespace RaspberryPi.BuildHat.Web.MotorControl.Models.Settings
{
    public class MotorControlSettings
    {
        public string BuildHatPort { get; set; } = string.Empty;
        public SensorPort MotorPort { get; set; } = SensorPort.PortA;
    }
}
