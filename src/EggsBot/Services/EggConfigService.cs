using EggsBot.Data;
using EggsBot.Toml;
using Serilog.Events;
using Tommy;

namespace EggsBot.Services;

public class EggConfigService
{
    private static readonly string ConfigPath = Path.Join(Directory.GetCurrentDirectory(), "config.toml");

    private EggConfig _config; 

    public EggConfigService()
    {
        _config = new EggConfig();
        
        if (!File.Exists(ConfigPath))
        {
            Serialize();
        }
        else
        {
            Deserialize();
            Serialize();
        }
    }

    private void Serialize()
    {
        string toml = TomlSerializer.Serialize(_config);
            
        File.WriteAllText(ConfigPath, toml.Trim());
    }

    private void Deserialize()
    {
        _config = TomlDeserializer.Deserialize(_config, File.ReadAllText(ConfigPath));
    }

    public string Token => _config.Token;

    public LogEventLevel LogLevel => _config.LogLevel;
    

    #region Config Keys

    private const string KeyToken = "token";
    private const string KeyLogLevel = "log_level";

    #endregion
    
    
}