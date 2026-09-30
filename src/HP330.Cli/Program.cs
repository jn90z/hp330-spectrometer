using HP330.Serial;

Console.WriteLine("HP330 Spectrometer diagnostic utility");
Console.WriteLine($"Known USB identity: VID_{Hp330UsbIdentity.VendorId}&PID_{Hp330UsbIdentity.ProductId}");
Console.WriteLine("Transmit commands are intentionally disabled until the HP330 protocol is verified.");
