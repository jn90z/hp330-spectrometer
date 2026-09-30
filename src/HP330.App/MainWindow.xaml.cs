using System.Windows;
using HP330.Serial;

namespace HP330.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        RefreshDevices();
    }

    private void Refresh_Click(object sender, RoutedEventArgs e) => RefreshDevices();

    private void RefreshDevices()
    {
        var devices = Hp330DeviceDiscovery.FindDevices();

        if (devices.Count == 0)
        {
            DeviceStatus.Text = "No HP330 detected";
            DeviceDetails.Text = "Connect the meter in Serial Communication mode and click Refresh.";
            return;
        }

        var device = devices[0];
        DeviceStatus.Text = $"{device.DisplayName} detected";
        DeviceDetails.Text = $"Port: {device.PortName}\nUSB: VID_{Hp330UsbIdentity.VendorId}&PID_{Hp330UsbIdentity.ProductId}\nPNP: {device.PnpDeviceId}";
    }
}
