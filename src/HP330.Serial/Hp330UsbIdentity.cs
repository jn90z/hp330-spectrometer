namespace HP330.Serial;

public static class Hp330UsbIdentity
{
    public const string VendorId = "0483";
    public const string ProductId = "5740";
    public const string PnpIdFragment = @"VID_0483&PID_5740";

    public static bool IsHp330(string? pnpDeviceId) =>
        pnpDeviceId?.Contains(PnpIdFragment, StringComparison.OrdinalIgnoreCase) == true;
}
