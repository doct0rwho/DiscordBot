using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal class MusicCommands : BaseCommandModule
    {
        [Command("play")]
        public async Task PlayCommand(CommandContext ctx, [RemainingText] string songName)
        {
            Console.WriteLine($"Получена команда play с песней: {songName}");

            if (ctx.Guild == null)
            {
                await ctx.RespondAsync("Я не могу выполнять команды в личных сообщениях. Пожалуйста, используйте сервер.");
                return;
            }

            await ctx.RespondAsync($".{songName}");
        }
        [Command("test")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Команда работает!");
        }
    }
}
