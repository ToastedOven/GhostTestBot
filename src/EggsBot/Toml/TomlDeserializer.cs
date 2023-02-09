using System.ComponentModel;
using System.Text.RegularExpressions;
using Tommy;

namespace EggsBot.Toml;

public static class TomlDeserializer
{
    public static T Deserialize<T>(T instance, string toml) where T : new()
    {
        using StringReader reader = new StringReader(toml);
        TomlTable table = TOML.Parse(reader);

        Type type = typeof(T);

        foreach (var propInfo in type.GetProperties())
        {
            string name = Regex.Replace(propInfo.Name, @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1")
                .Trim()
                .Replace(' ', '-')
                .ToLower();
            
            if (!table.HasKey(name))
                continue;
            
            TomlNode node = table[name];
            Type propType = propInfo.PropertyType;
            
            propInfo.SetValue(instance, ParseNode(node, propType));
        }

        return instance;
    }

    private static object ParseNode(TomlNode node, Type targetType)
    {
        if (targetType == typeof(string))
            return (string)node;
        if (targetType == typeof(bool))
            return (bool)node;
        if (targetType == typeof(int))
            return (int)node;
        if (targetType == typeof(long))
            return (long)node;
        if (targetType == typeof(float))
            return (float)node;
        if (targetType == typeof(double))
            return (double)node;
        if (targetType == typeof(DateTime))
            return (DateTime)node;
        if (targetType == typeof(DateTimeOffset))
            return (DateTimeOffset)node;
        if (targetType.IsEnum)
            return Enum.Parse(targetType, node);

        throw new InvalidCastException();
    }
}