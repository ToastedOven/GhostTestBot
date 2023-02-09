using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using EggsBot.Toml.Attributes;
using Tommy;

namespace EggsBot.Toml;

public static class TomlSerializer
{
    public static string Serialize<T>(T instance)
    {
        Type instanceType = typeof(T);

        TomlTable table = new TomlTable();

        foreach (var propInfo in instanceType.GetProperties())
        {
            string name = Regex.Replace(propInfo.Name, @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1")
                .Trim()
                .Replace(' ', '-')
                .ToLower();

            object value = propInfo.GetValue(instance) ?? throw new NullReferenceException();

            TomlNode node = CreateNode(value);

            foreach (var attribute in propInfo.GetCustomAttributes())
            {
                if (attribute is not TomlCommentAttribute commentAttribute)
                    continue;

                if (!string.IsNullOrEmpty(node.Comment))
                {
                    string comment = node.Comment;

                    node.Comment = commentAttribute.Comment + "\n" + comment;
                }
                else
                {
                    node.Comment = commentAttribute.Comment;
                }
            }

            table[name] = node;
        }

        StringBuilder builder = new StringBuilder();
        using StringWriter writer = new StringWriter(builder);
        
        table.WriteTo(writer);

        return builder.ToString();
    }

    private static TomlNode CreateNode(object value)
    {
        
        return value switch
        {
            string text => text,
            bool val => val,
            int num => num,
            long num => num,
            float num => num,
            double num => num,
            DateTime dateTime => dateTime,
            DateTimeOffset dateTimeOffset => dateTimeOffset,
            Enum val => ParseEnum(val),
            _ => throw new InvalidCastException()
        };
    }

    private static TomlNode ParseEnum(Enum targetEnum)
    {
        StringBuilder comment = new StringBuilder();
        comment.AppendLine("Valid values:");
        foreach (var value in Enum.GetValues(targetEnum.GetType()))
        {
            string name = Enum.GetName(targetEnum.GetType(), value) ?? throw new NullReferenceException();

            comment.Append($"{name}: {(int)value}, ");
        }
        comment.Remove(comment.Length - 2, 2);

        TomlNode node = (int)Enum.ToObject(targetEnum.GetType(), targetEnum);
        node.Comment = comment.ToString();
        return node;
    }
}