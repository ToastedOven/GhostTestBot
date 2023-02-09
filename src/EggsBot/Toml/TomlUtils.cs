using System.Reflection;
using EggsBot.Toml.Attributes;

namespace EggsBot.Toml;

public static class TomlUtils
{
    static TomlUtils()
    {
        // listen, just... just don't look here for a bit okay, please?

        foreach (var attribute in Assembly.GetExecutingAssembly().GetCustomAttributes())
        {
            //todo
        }
    }
}