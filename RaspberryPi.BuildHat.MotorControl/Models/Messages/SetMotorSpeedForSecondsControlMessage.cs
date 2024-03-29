namespace RaspberryPi.BuildHat.Web.MotorControl.Models.Messages
{
    public class SetMotorSpeedForSecondsControlMessage: IMotorControlMessage
    {
        public int Seconds { get; set; } = 0;
    }
}
