using Microsoft.Win32;

namespace HP330.Serial;

public static class Hp330DeviceDiscovery
{
    private const string SerialCommRegistryPath = @"HARDWARE\DEVICEMAP\SERIALCOMM";

    public static IReadOnlyList<Hp330PortInfo> FindDevices()
    {
        if (!OperatingSystem.IsWindows())
            return Array.Empty<Hp330PortInfo>();

        var ports = new List<Hp330PortInfo>();

        using var serialComm = Registry.LocalMachine.OpenSubKey(SerialCommRegistryPath);
        if (serialComm is null)
            return ports;

        foreach (var valueName in serialComm.GetValueNames())
        {
            if (serialComm.GetValue(valueName) is not string portName)
                continue;

            // The registry value name commonly contains the USB device path and
            // therefore the VID/PID. Do not open the COM port during discovery.
            if (!Hp330UsbIdentity.IsHp330(valueName))
                continue;

            ports.Add(new Hp330PortInfo(
                portName,
                $"HP330 ({portName})",
                valueName));
        }

        return ports.OrderBy(p => p.PortName, StringComparer.OrdinalIgnoreCase).ToArray();
    }
}
