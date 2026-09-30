using HP330.Serial;
namespace HP330.Tests;
public sealed class Hp330UsbIdentityTests
{
    [Fact]
    public void MatchesVerifiedPhysicalMeterPnpId()
    {
        Assert.True(Hp330UsbIdentity.IsHp330(@"USB\VID_0483&PID_5740\3267368C3433"));
    }
}
