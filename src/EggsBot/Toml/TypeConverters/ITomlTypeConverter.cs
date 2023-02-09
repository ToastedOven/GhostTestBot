using Tommy;

namespace EggsBot.Toml.TypeConverters;

public interface ITomlTypeConverter<TType>
{
    public TType Deserialize(TomlNode tomlNode);

    public RawTomlNode Serialize(TType instance);
}