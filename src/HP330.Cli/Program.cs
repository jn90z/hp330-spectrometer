using HP330.Serial;

Console.WriteLine("HP330 Spectrometer diagnostic utility");
Console.WriteLine($"Known USB identity: VID_{Hp330UsbIdentity.VendorId}&PID_{Hp330UsbIdentity.ProductId}");
Console.WriteLine();

var devices = Hp330DeviceDiscovery.FindDevices();

if (devices.Count == 0)
{
    Console.WriteLine("No HP330 USB serial device detected.");
}
else
{
    foreach (var device in devices)
    {
        Console.WriteLine($"Detected: {device.DisplayName}");
        Console.WriteLine($"  Port: {device.PortName}");
        Console.WriteLine($"  PNP:  {device.PnpDeviceId}");
    }
}

Console.WriteLine();
Console.WriteLine("SAFE MODE: no bytes were transmitted to any serial port.");
