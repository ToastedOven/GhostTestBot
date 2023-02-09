namespace EggsBot.Toml;

public readonly struct RawTomlNode
{
    public readonly string Toml;
    public readonly string? Metadata;

    public RawTomlNode(string toml, string? metadata = null)
    {
        Toml = toml;
        Metadata = metadata;
    }
}