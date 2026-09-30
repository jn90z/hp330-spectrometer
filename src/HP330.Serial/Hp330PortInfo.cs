namespace HP330.Serial;

public sealed record Hp330PortInfo(
    string PortName,
    string DisplayName,
    string PnpDeviceId,
    string? DeviceInstanceId = null);
