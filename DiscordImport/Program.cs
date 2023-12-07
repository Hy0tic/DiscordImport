// See https://aka.ms/new-console-template for more information
using DiscordImport;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    
var token = configuration.GetSection("token").Value;

var _discord = DiscordClient.Instance
    .SetToken(token);

_discord.RunBotAsync().GetAwaiter().GetResult();