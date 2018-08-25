using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;

namespace serwitor
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
        static VoiceNextClient voice;

        static void Main(string[] args)
        {
            
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
      //      voice = discord.UseVoiceNext();
        }

        static async Task MainAsync(string[] args)
        {

            discord = new DiscordClient(new DiscordConfig
            {
                Token = "TUTAJ_NALEŻY_UMIEŚCIĆ_TOKEN_DISCORD",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!");
              
                };

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<MyCommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}



