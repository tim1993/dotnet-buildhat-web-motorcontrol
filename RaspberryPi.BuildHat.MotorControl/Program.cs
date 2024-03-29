using RaspberryPi.BuildHat.Web.MotorControl;
using RaspberryPi.BuildHat.Web.MotorControl.Models.Settings;
using RaspberryPi.BuildHat.Web.MotorControl.Services;

await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        var settingsService = new SettingsService();
        MotorControlSettings motorControlSettings = settingsService.GetMotorControlSettings();
        HostingSettings hostingSettings = settingsService.GetHostingSettings();

        webBuilder.UseStartup<Startup>();
        webBuilder.UseUrls([$"http://0.0.0.0:{hostingSettings.Port}"]);
        webBuilder.ConfigureServices(services =>
        {
            services.AddSingleton(x => new MotorControlService(motorControlSettings.BuildHatPort, motorControlSettings.MotorPort));
        });
    })
    .Build()
    .RunAsync();

