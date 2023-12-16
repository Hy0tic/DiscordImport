// See https://aka.ms/new-console-template for more information
using DiscordImport;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var token = configuration["token"];
var openAIkey = configuration["openAIkey"];



var discord = DiscordClient.Instance
    .SetOpenAIkey(openAIkey)
    .SetToken(token);

discord.RunBotAsync().GetAwaiter().GetResult();