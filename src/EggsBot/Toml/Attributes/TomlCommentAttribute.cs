namespace EggsBot.Toml.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class TomlCommentAttribute : Attribute
{
    public readonly string Comment;

    public TomlCommentAttribute(string comment)
    {
        Comment = comment;
    }
}