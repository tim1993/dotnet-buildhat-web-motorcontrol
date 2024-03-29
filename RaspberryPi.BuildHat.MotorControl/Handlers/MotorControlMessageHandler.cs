using RaspberryPi.BuildHat.Web.MotorControl.Models.Messages;
using RaspberryPi.BuildHat.Web.MotorControl.Services;
using System.Text.Json;

namespace RaspberryPi.BuildHat.Web.MotorControl.Handlers
{
    public static class MotorControlMessageHandler
    {
        public static async Task HandleMotorControlMessage<T>(HttpContext context, MotorControlService motorControlService) where T : IMotorControlMessage
        {
            await Console.Out.WriteLineAsync("Set motor speed message received");

            var request = context.Request;
            var stream = new StreamReader(request.Body);
            var body = await stream.ReadToEndAsync();

            var parsedMessage = JsonSerializer.Deserialize<T>(body);
            if (parsedMessage != null)
            {
                motorControlService.HandleMotorControlMessage(parsedMessage);
            }

            return;
        }
    }
}
