namespace LibCx9020;
public class CxDaten
{
    public enum DatenBereich
    {
        Di = 0,
        Da = 1,
        Ai = 2,
        Aa = 3
    }

    public string? VersionsStringPlc { get; set; }
    public byte[] BefehlePlc { get; } = new byte[1024];
    public byte[] Di { get; } = new byte[1024];
    public byte[] Da { get; } = new byte[1024];
    public byte[] Ai { get; } = new byte[1024];
    public byte[] Aa { get; } = new byte[1024];

    public byte GetByte(DatenBereich datenBereich, int index) => datenBereich switch
    {
        DatenBereich.Di => Di[index],
        DatenBereich.Da => Da[index],
        DatenBereich.Ai => Ai[index],
        DatenBereich.Aa => Aa[index],
        _ => throw new ArgumentOutOfRangeException(nameof(datenBereich), datenBereich, null)
    };
    public int GetInt(DatenBereich datenBereich, int index) => datenBereich switch
    {
        DatenBereich.Di => (256 * Di[1 + index]) + Di[index],
        DatenBereich.Da => (256 * Da[1 + index]) + Da[index],
        DatenBereich.Ai => (256 * Ai[1 + index]) + Ai[index],
        DatenBereich.Aa => (256 * Aa[1 + index]) + Aa[index],
        _ => throw new ArgumentOutOfRangeException(nameof(datenBereich), datenBereich, null)
    };
}
