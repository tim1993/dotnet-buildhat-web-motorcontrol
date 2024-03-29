using RaspberryPi.BuildHat.Web.MotorControl.Handlers;
using RaspberryPi.BuildHat.Web.MotorControl.Models.Messages;
using RaspberryPi.BuildHat.Web.MotorControl.Services;

namespace RaspberryPi.BuildHat.Web.MotorControl
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                MotorControlService motorControlService = serviceProvider.GetService<MotorControlService>() ?? throw new InvalidOperationException($"{nameof(MotorControlService)} is not injected");

                endpoints.MapGet("/", () => "MotorControl is running...");
                endpoints.MapPost("/motorcontrol/speed", (context) => MotorControlMessageHandler.HandleMotorControlMessage<SetMotorSpeedControlMessage>(context, motorControlService));
                endpoints.MapPost("/motorcontrol/runseconds", (context) => MotorControlMessageHandler.HandleMotorControlMessage<SetMotorSpeedForSecondsControlMessage>(context, motorControlService));
            });
        }
    }
}
