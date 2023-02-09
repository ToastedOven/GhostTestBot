using Discord;
using Discord.Commands;
using Discord.WebSocket;
using EggsBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace EggsBot;

internal class Program
{
    private readonly IServiceProvider _services;

    private Program()
    {
        _services = ConfigureServices();
    }
    
    public static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    public async Task MainAsync()
    {
        var config = _services.GetRequiredService<EggConfigService>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(config.LogLevel)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
        var client = _services.GetRequiredService<DiscordSocketClient>();
        
        client.Log += LogAsync;

        await client.LoginAsync(TokenType.Bot, config.Token);
        await client.StartAsync();

        async Task MessageReceived(SocketMessage message)
        {
            Console.WriteLine($"{message.Author.Username} wrote: {message.Content}");
        }
        client.MessageReceived += MessageReceived;
        
        
        await Task.Delay(Timeout.Infinite);
    }

    private async Task LogAsync(LogMessage message)
    {
        LogEventLevel severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };
        
        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        await Task.CompletedTask;
    }

    private static IServiceProvider ConfigureServices()
    {
        DiscordSocketConfig config = new DiscordSocketConfig
        {

        };
        
        return new ServiceCollection()
            .AddSingleton<EggConfigService>()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()    
            .AddSingleton<CommandService>()
            .BuildServiceProvider();
    }
}