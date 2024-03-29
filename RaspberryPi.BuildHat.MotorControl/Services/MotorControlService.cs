using Iot.Device.BuildHat;
using Iot.Device.BuildHat.Models;
using RaspberryPi.BuildHat.Web.MotorControl.Models.Messages;
using System.Globalization;

namespace RaspberryPi.BuildHat.Web.MotorControl.Services
{
    public class MotorControlService : IDisposable
    {
        private readonly Brick _buildHat;

        private readonly string _serialPort;
        private readonly SensorPort _motorPort;

        public MotorControlService(string serialPort, SensorPort motorPort)
        {
            _serialPort = serialPort;
            _motorPort = motorPort;

            _buildHat = new(_serialPort);
            PrintVersionInfos();
        }

        public void Dispose()
        {
            Console.WriteLine($"{nameof(MotorControlService)} disposing");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void HandleMotorControlMessage<T>(T message) where T : IMotorControlMessage
        {
            if (message == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(_serialPort))
            {
                throw new ArgumentException($"Please instantiate {nameof(MotorControlService)} with the correct serial port {nameof(_serialPort)}");
            }

            switch(message)
            {
                case SetMotorSpeedControlMessage:
                    SetMotorSpeed(message.MotorSpeed);
                    break;
                case SetMotorSpeedForSecondsControlMessage:
                    var motorSpeedForSecondsMessage = message as SetMotorSpeedForSecondsControlMessage ?? throw new InvalidCastException(message.ToString());
                    RunMotorForSeconds(motorSpeedForSecondsMessage.Seconds, motorSpeedForSecondsMessage.MotorSpeed);
                    break;
                default:
                    throw new NotImplementedException("Message Type not implemented");
            }
        }

        private void SetMotorSpeed(int speed)
        {
            Console.WriteLine($"Sending motorspeed command, speed: {speed}, port {_motorPort}");

            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; plimit 1\r");
            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; bias 0.1\r");
            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; pwm ; set {(speed / 100.0).ToString(CultureInfo.InvariantCulture)}\r");
        }

        private void RunMotorForSeconds(int seconds, int speed)
        {
            Console.WriteLine($"Sending run for seconds command, seconds: {seconds}, speed: {speed}");
            if (seconds <= 0)
            {
                // No need to move!
                return;
            }

            if (speed == 0)
            {
                throw new ArgumentException("Speed can't be 0");
            }

            speed = speed < -100 ? -100 : speed > 100 ? 100 : speed;

            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; plimit 1\r");
            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; bias 0.1\r");
            _buildHat.SendRawCommand($"port {(byte)_motorPort} ; pid {(byte)_motorPort} 0 0 s1 1 0 0.003 0.01 0 100; set pulse {speed} 0.0 {seconds.ToString(CultureInfo.InvariantCulture)} 0\r");
        }

        private void PrintVersionInfos()
        {
            var info = _buildHat.BuildHatInformation;
            Console.WriteLine($"version: {info.Version}, firmware date: {info.FirmwareDate}, signature:");
            Console.WriteLine($"{BitConverter.ToString(info.Signature)}");
            Console.WriteLine($"Vin = {_buildHat.InputVoltage.Volts} V");
        }

        protected virtual void Dispose(bool disposing)
        {
            if ( disposing )
            {
                _buildHat?.Dispose();
            }
        }
    }
}
