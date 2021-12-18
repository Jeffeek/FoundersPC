namespace FoundersPC.SharedKernel.Extensions;

public static class NumberExtensions
{
    private const int DefaultCoefficient = 1024;
    private const int BitInByte = 8;

    // Down
    public static long FromGigabytesToMegabytes(this int source) => source * DefaultCoefficient;
    public static long FromGigabytesToKilobytes(this int source) => source.FromGigabytesToMegabytes() * DefaultCoefficient;
    public static long FromGigabytesToBytes(this int source) => source.FromGigabytesToKilobytes() * DefaultCoefficient;
    public static long FromGigabytesToBits(this int source) => source.FromGigabytesToBytes() * BitInByte;

    public static long FromMegabytesToKilobytes(this int source) => source * DefaultCoefficient;
    public static long FromMegabytesToBytes(this int source) => source.FromMegabytesToKilobytes() * DefaultCoefficient;
    public static long FromMegabytesToBits(this int source) => source.FromMegabytesToBytes() * BitInByte;

    public static long FromKilobytesToBytes(this int source) => source * DefaultCoefficient;
    public static long FromKilobytesToBits(this int source) => source.FromKilobytesToBytes() * BitInByte;

    //Up
    public static decimal FromMegabytesToGigabytes(this long source) => (decimal)source / DefaultCoefficient;
    public static decimal FromKilobytesToGigabytes(this long source) => source.FromMegabytesToGigabytes() / DefaultCoefficient;
    public static decimal FromBytesToGigabytes(this long source) => source.FromKilobytesToGigabytes() / DefaultCoefficient;
    public static decimal FromBitsToGigabytes(this long source) => source.FromBytesToGigabytes() / BitInByte;

    public static decimal FromKilobytesToMegabytes(this long source) => source * DefaultCoefficient;
    public static decimal FromBytesToMegabytes(this long source) => source.FromKilobytesToMegabytes() / DefaultCoefficient;
    public static decimal FromBitsToMegabytes(this long source) => source.FromBytesToMegabytes() / BitInByte;

    public static decimal FromBytesToKilobytes(this long source) => source * DefaultCoefficient;
    public static decimal FromBitsToKilobytes(this long source) => source.FromBytesToKilobytes() / BitInByte;
}