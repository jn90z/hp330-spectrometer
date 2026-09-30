# HP330 Spectrometer

Reverse-engineering and Windows integration project for the HP330 / HPCS-330 handheld spectrometer.

The goal is to communicate directly with the instrument over USB, automate measurements, log results, and provide a Windows desktop GUI without depending on the vendor application.

## Current status

The vendor installer `OHSP330_Setup1.60.9-241213.exe` has been unpacked and analyzed.

Confirmed so far:

- Vendor application: `HPCS-330X.exe`
- Spectral/calculation library: `OHSPEng.dll`
- USB communication is exposed to Windows as a virtual COM / serial interface
- Microsoft `usbser.sys` is used
- Observed USB hardware ID: `USB\VID_0483&PID_5740`
- The vendor application directly uses the Windows serial APIs
- 9600-baud and 2400-baud serial initialization paths exist in the application; their exact model assignment is still being verified
- The software supports multiple HPCS-330 variants
- Full 1 nm spectral data is represented internally as `Spect1nmData`
- HP330 CSV exports contain 401 spectral samples covering 380–780 nm plus calculated photometric/colorimetric results

The exact HPCS-330 request/response command protocol is still being reverse-engineered. Packet observations from unrelated supported instruments are intentionally not treated as HP330 commands until verified.

See [docs/protocol-notes.md](docs/protocol-notes.md) for the working protocol map.

## Intended functionality

The planned Windows application will eventually support:

- Automatic HP330 discovery
- USB/COM connection
- Triggering measurements
- CCT
- Illuminance
- CRI / Ra
- R1–R15 including R9
- CIE chromaticity coordinates
- Duv
- SDCM
- Peak/dominant wavelength
- Irradiance
- 380–780 nm spectral graph
- Raw packet logging
- CSV/data logging
- Configurable pass/fail limits
- Production-test workflows

## Hardware testing

For live protocol verification, the preferred capture is:

1. HP330 in **Serial Communication** mode
2. Start a USBPcap/Wireshark capture
3. Launch the official HPCS-330X software
4. Connect to the instrument
5. Perform exactly one measurement
6. Save the corresponding HP330 CSV
7. Disconnect
8. Stop the capture

A matching `.pcapng` + CSV allows the raw USB/serial traffic to be correlated with known measurement and spectral values.

## Safety / development rule

Until the protocol is fully understood, development tools should favor read/query operations and preserve raw traffic. Unknown commands should not be sent to physical hardware without understanding their purpose, because the vendor software may also contain calibration, configuration, or firmware-update commands.
