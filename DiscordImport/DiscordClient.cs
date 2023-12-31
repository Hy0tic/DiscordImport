﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ChatGptNet;

namespace DiscordImport{
   public class DiscordClient
   {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider? _services;
        private string _token;
        private string openAIkey;

        private static readonly Lazy<DiscordClient> _lazyDiscordClient = new Lazy<DiscordClient>(() => new DiscordClient());
        private DiscordClient()
        {
            _commands = new CommandService();
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All
            });

        }

        public static DiscordClient Instance
        {
            get{
                return _lazyDiscordClient.Value;
            }
        }

        public DiscordClient SetToken(string token)
        {
            _token = token;
            return this;
        }

        public DiscordClient SetOpenAIkey(string apiKey)
        {
            openAIkey = apiKey;
            return this;
        }

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .ConfigureServices()
                .AddChatGpt(options =>
                {
                    options.UseOpenAI(apiKey: openAIkey);
                })
                .Services
                .BuildServiceProvider();

            _client.Log += Client_log;
            
            await RegisterCommandAsync();
            await _client.LoginAsync(TokenType.Bot,_token);
            await _client.StartAsync();
            await _client.SetGameAsync("beep boop");
            await _client.SetStatusAsync(UserStatus.DoNotDisturb);
            await Task.Delay(-1);
        }

        private async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(),_services);
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
            var msg = (SocketUserMessage)message;
            var context = new SocketCommandContext(_client,msg);
            Console.WriteLine(msg);
            if (msg.Author.IsBot) return;

            int argPos = 0;
            if(msg.HasStringPrefix(".", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context,argPos,_services);
                if(!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if(result.Error.Equals(CommandError.UnmetPrecondition)) await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
            
        }

        private Task Client_log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task ReportMessageToHost(SocketMessage msg)
        {
            var host = await _client.GetUserAsync(451176601644957698);
            var channel = await host.CreateDMChannelAsync();
            await channel.SendMessageAsync(msg.ToString() + " -" + msg.Author);
        }
   }

}