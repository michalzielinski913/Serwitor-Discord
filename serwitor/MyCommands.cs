using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.VoiceNext;
using System.Net;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace serwitor
{
    public class MyCommands
    {
        [Command("hi"), Description("Hi Back")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
        }

        [Command("random")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(min, max)}");
        }

        [Command("ping")][Aliases("pong")][Description("Latency check")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.RespondAsync($":ping_pong: Pong! Ping: {ctx.Client.Ping}ms");
        }
        [Command("cat"), Aliases("kot"), Description("Random cat photo")]
        public async Task catj(CommandContext ctx)
        {
            string json;
            using (WebClient webClient = new System.Net.WebClient())
            {
                WebClient n = new WebClient();
                json = n.DownloadString("https://aws.random.cat/meow");
                string valueOriginal = Convert.ToString(json);
   
                    json = json.Remove(json.IndexOf("{\"file\":\""), "{\"file\":\"".Length);  
                    json = json.Remove(json.IndexOf("\"}"), "\"}".Length);          
                    json = json.Replace(@"\", @"/");
                    json = json.Replace(@"////", @"//");
                    json = json.Replace(@"//i//", @"/i/");
         
            }
         
               var embed = new DiscordEmbed
                {
                    Title = "Cat",
                    Image = new DiscordEmbedImage
                    {
                        Url = (json)
                    }
                };
                await ctx.RespondAsync("", embed: embed);
        }

        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNextClient();

            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc != null)
                throw new InvalidOperationException("Already connected in this guild.");

            var chn = ctx.Member?.VoiceState?.Channel;
            if (chn == null)
                throw new InvalidOperationException("You need to be in a voice channel.");

            vnc = await vnext.ConnectAsync(chn);
            await ctx.RespondAsync("👌");
        }

        [Command("leave")]
        public async Task Leave(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNextClient();

            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc == null)
                throw new InvalidOperationException("Not connected in this guild.");

            vnc.Disconnect();
            await ctx.RespondAsync("👌");
        }

    }
}
