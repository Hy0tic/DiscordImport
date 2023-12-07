// See https://aka.ms/new-console-template for more information
using DiscordImport;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var token = "";
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .Build();

var _discord = DiscordClient.Instance
    .SetToken(token);

_discord.RunBotAsync().GetAwaiter().GetResult();