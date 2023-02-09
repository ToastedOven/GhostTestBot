using EggsBot.Toml.Attributes;
using Serilog.Events;

namespace EggsBot.Data;

public class EggConfig
{
    [TomlComment("Discord bot token")]
    public string Token { get; set; } = "TOKEN";

    [TomlComment("Minimum log level")]
    public LogEventLevel LogLevel { get; set; } = LogEventLevel.Verbose;
}