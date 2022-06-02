using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDoGui.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Felipe")]
        public async Task Ping()
        {
            ReplyAsync("Tabaia");
        }

      
    }
}
