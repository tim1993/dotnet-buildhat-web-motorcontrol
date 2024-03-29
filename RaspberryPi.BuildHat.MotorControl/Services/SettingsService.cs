using RaspberryPi.BuildHat.Web.MotorControl.Models.Settings;

namespace RaspberryPi.BuildHat.Web.MotorControl.Services
{
    internal class SettingsService
    {
        private readonly IConfiguration _configuration;

        public SettingsService() 
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        }

        public MotorControlSettings GetMotorControlSettings()
        {
            var motorControlSettings = _configuration.GetRequiredSection("MontorControlSettings").Get<MotorControlSettings>();
            return motorControlSettings ?? throw new InvalidOperationException("MotorControlSettings couldn't be loaded");
        }

        public HostingSettings GetHostingSettings()
        {
            var hostingSettings = _configuration.GetRequiredSection("HostingSettings").Get<HostingSettings>();
            return hostingSettings ?? throw new InvalidOperationException("HostingSettings couldn't be loaded");
        } 
    }
}
