namespace HP330.Protocol;

public sealed record SpectrumPoint(double WavelengthNm, double Value);

public sealed record Hp330Measurement(
    double IlluminanceLux,
    double IrradianceWattsPerSquareMeter,
    double CctKelvin,
    double Ra,
    double R9,
    double ChromaticityX,
    double ChromaticityY,
    double Duv,
    double Sdcm,
    IReadOnlyList<SpectrumPoint> Spectrum);
