namespace EggsBot.Toml.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TomlTypeConverterAttribute : Attribute
{
    public readonly Type TargetType;

    public TomlTypeConverterAttribute(Type targetType)
    {
        TargetType = targetType;
    }
}