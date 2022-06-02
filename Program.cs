using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;


namespace Bot
{
    internal class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = "OTgxMzIzODcwNTEyNTEzMDM0.GeRoYS.NvJ9AfkLGnafv_2MBC5njHnPR6o94BcsS9SIKM";

            _client.Log += _client_Log;

            await RegisterCommandAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);


        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }

            context = new SocketCommandContext(_client, message as SocketUserMessage);
            await context.Message.AddReactionAsync(new Emoji("🇹")).ConfigureAwait(false);
            await context.Message.AddReactionAsync(new Emoji("🆎")).ConfigureAwait(false);
            await context.Message.AddReactionAsync(new Emoji("🇦")).ConfigureAwait(false);
            await context.Message.AddReactionAsync(new Emoji("🇮")).ConfigureAwait(false);
            await context.Message.AddReactionAsync(new Emoji("🅰️")).ConfigureAwait(false);

        }

    }
}


