using HP330.Serial;

namespace HP330.Tests;

public sealed class Hp330UsbIdentityTests
{
    [Fact]
    public void MatchesVerifiedPhysicalMeterPnpId()
    {
        Assert.True(Hp330UsbIdentity.IsHp330(@"USB\VID_0483&PID_5740\3267368C3433"));
    }

    [Fact]
    public void RejectsOtherUsbSerialDevices()
    {
        Assert.False(Hp330UsbIdentity.IsHp330(@"USB\VID_1234&PID_5678\TEST"));
    }

    [Fact]
    public void MatchIsCaseInsensitive()
    {
        Assert.True(Hp330UsbIdentity.IsHp330(@"usb\vid_0483&pid_5740\3267368c3433"));
    }
}
