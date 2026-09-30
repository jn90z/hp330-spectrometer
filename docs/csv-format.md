# HP330 CSV Export Notes

These notes are based on CSV files exported directly by an HP330.

## General format

Observed exports contain instrument/model metadata, calculated photometric and colorimetric values, individual CRI values, integration/signal information, and a complete 1 nm spectral dataset from 380 through 780 nm (**401 samples**).

Observed files may be padded with NUL bytes after the useful text. Parsers should tolerate this rather than assuming a conventional text-only CSV ending immediately after the last record.

## Observed measurement field IDs

The first numeric value associated with many result records appears to behave like an internal field identifier.

| ID | Observed field |
|---:|---|
| 0 | Illuminance |
| 1 | Irradiance |
| 19 | x |
| 20 | y |
| 21 | u |
| 22 | v |
| 23 | u' |
| 24 | v' |
| 28 | Peak wavelength |
| 29 | Central wavelength |
| 30 | Dominant wavelength |
| 31 | Centroid wavelength |
| 32 | Half width |
| 34 | CRI Ra |
| 35 | Ravg |
| 36–50 | R1–R15 |
| 44 | R9 |
| 57 | CCT |
| 58 | Purity |
| 59 | SDCM |
| 60 | Duv |
| 61 | S/P |
| 78 | Dark signal |
| 79 | Peak signal |
| 80 | Integration time |

This mapping is currently **observational**. It should not yet be assumed to be identical to on-wire protocol field IDs.

## Protocol-analysis value

A CSV generated from the exact same measurement as a USB packet capture gives us known values to search for in the binary response. The 401-point spectrum is particularly useful for identifying packet boundaries, numeric encoding, scaling, byte order, wavelength ordering, and packet chunking.
