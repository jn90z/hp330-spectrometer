# HP330 Spectrometer Protocol Notes

Status: reverse engineering in progress.

## Confirmed from vendor installer/software

- Installer: `OHSP330_Setup1.60.9-241213.exe`
- Installer family: NSIS 3.10, PE32
- Main application: `HPCS-330X.exe`
- Spectral/calculation library: `OHSPEng.dll`
- Windows transport: virtual COM / serial over USB
- Windows serial APIs observed in the application:
  - `GetCommState`
  - `SetCommState`
  - `GetCommTimeouts`
  - `SetCommTimeouts`
  - `SetupComm`
  - `ClearCommError`
- COM-port enumeration path observed:
  - `HARDWARE\\DEVICEMAP\\SERIALCOMM`
- USB CDC driver path uses Microsoft `usbser.sys`
- Observed USB hardware ID:
  - `USB\\VID_0483&PID_5740`
- Supported model strings observed include:
  - HPCS-330
  - HPCS-330P
  - HPCS-330UV
  - HPCS-330PR
  - HPCS-330Pro
  - HPCS-330IR
- Full 1 nm spectrum storage field observed:
  - `Spect1nmData`

## Serial configuration observed

Two serial initialization paths were found in the vendor executable:

### Path A
- Baud: 9600
- Data bits: 8
- Stop bits: 1
- Parity: caller supplied / not yet mapped
- Opens COM port for read/write
- Writes caller-provided command buffer

### Path B
- Baud: 2400
- Data bits: 8
- Stop bits: 1
- Parity: caller supplied / not yet mapped
- RX/TX queues: approximately 40960 bytes
- Long read timeout behavior observed

Current working hypothesis: Path A is more likely to be relevant to HPCS-330, but this is **not yet confirmed**.

## Unidentified protocol branch

A separate supported-device branch sends:

```text
50 00 00 50
```

A 14-byte response parser was also found where:

```text
response[13] == sum(response[0..12]) & 0xFF
```

This packet is **not yet attributed to HPCS-330** and must not be treated as an HP330 command until the caller chain is confirmed.

## Measurement/data fields observed in application

The vendor software contains storage/UI paths for values including:

- Illuminance / lux
- CCT
- Chromaticity x/y
- u/v or u'/v' family coordinates
- Duv
- CRI Ra
- Individual Ri values including R9
- SDCM
- Peak wavelength
- Dominant wavelength
- Integration time
- Flicker/electrical-related fields
- Full spectral power distribution at 1 nm spacing

## Next reverse-engineering targets

1. Identify the exact HPCS-330 model-selection branch.
2. Trace that branch to the first serial `WriteFile` call.
3. Recover literal request bytes for:
   - identify/query
   - start measurement
   - fetch result
   - fetch spectral data
4. Recover response framing and length fields.
5. Determine checksum/CRC.
6. Map returned offsets to measurement fields.
7. Build a minimal Windows serial probe before the full GUI.

## Rule for this document

Only promote findings to **confirmed** when directly tied to the HPCS-330 code path or verified against a physical meter. Keep observations from other supported instruments separated.
