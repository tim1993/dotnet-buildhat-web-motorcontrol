# RaspberryPi BuildHAT Web MotorControl
This project enables you to control up to four LEGO® TECHNIC™ motors with Raspberry Pi add-on board Build HAT over the web.

# Getting started
[Install .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime)

You can configure the project using appsettings.json:

```
{
  "HostingSettings": {
    "Port": "<WebServer Port>"
  },
  "MontorControlSettings": {
    "BuildHatPort": "<Serial Port>", // e.g. /dev/serial0
    "MotorPort": "<MotorPorts to use from Build HAT>" // PortA, PortB, PortC, PortD
  }
}
```

# Build and run
To build and run the application you have to:

Run ```dotnet restore```
then ```dotnet run```

# Links
[Build HAT Documentation](https://www.raspberrypi.com/documentation/accessories/build-hat.html)
[Using Build HAT from .NET](https://github.com/raspberrypi/documentation/blob/develop/documentation/asciidoc/accessories/build-hat/net-brick.adoc)
[Python Build HAT](https://github.com/RaspberryPiFoundation/python-build-hat)