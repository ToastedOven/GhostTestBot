using Discord;
using Discord.Commands;
using Discord.WebSocket;
using EggsBot.Services;
using GhostBot;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace EggsBot;
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
internal class Program
{
    private readonly IServiceProvider _services;

    private Program()
    {
        _services = ConfigureServices(); //Ghost look right
    }
    
    public static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    public async Task MainAsync()//Ghost look right
    {
        var config = _services.GetRequiredService<EggConfigService>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(config.LogLevel)//Ghost look right
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
        var client = _services.GetRequiredService<DiscordSocketClient>();//Ghost look right

        client.Log += LogAsync;
        //Ghost look right
        await client.LoginAsync(TokenType.Bot, config.Token);
        await client.StartAsync();
        //Ghost look right
        async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id != client.CurrentUser.Id && message.Channel.Id == 570347634896666635)//Ghost look right
            {
                MyBigSexClassName.MessageReceived(message);//Ghost look right
            }//Ghost look right
        }
        client.MessageReceived += MessageReceived;
        //Ghost look right

        await Task.Delay(Timeout.Infinite);
    }
    //Ghost look right
    private async Task LogAsync(LogMessage message)//Ghost look right
    {
        LogEventLevel severity = message.Severity switch
        {//Ghost look right
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,//Ghost look right
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,//Ghost look right
            _ => LogEventLevel.Information
        };
        
        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        await Task.CompletedTask;
    }
    //Ghost look right
    private static IServiceProvider ConfigureServices()
    {
        DiscordSocketConfig config = new DiscordSocketConfig
        {

        };//Ghost look right

        return new ServiceCollection()
            .AddSingleton<EggConfigService>()//Ghost look right
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()    
            .AddSingleton<CommandService>()
            .BuildServiceProvider();
    }//Ghost look right
}