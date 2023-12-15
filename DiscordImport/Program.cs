// See https://aka.ms/new-console-template for more information
using DiscordImport;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var token = configuration["token"];

var discord = DiscordClient.Instance
    .SetToken(token);

discord.RunBotAsync().GetAwaiter().GetResult();