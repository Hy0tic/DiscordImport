using System.Net.Http.Headers;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordImport.Modules;
public class MiscellaneousCommand:ModuleBase<SocketCommandContext>
{
    private readonly CommandService _commands;
    public MiscellaneousCommand(CommandService command)
    {
        _commands = command;
    }

    [Command("roll")]
    [Summary("Return a random number from 0 to specified number, if no number is specified it rolls between 0 to 100.")]
    public async Task DiceRoll(string input = "")
    {
        try
        {
            var _input = long.Parse(input);
            var rnd = new Random().NextInt64(_input+1).ToString();
            await ReplyAsync(rnd);  
        }
        catch
        {
            var rnd = new Random().NextInt64(100).ToString();
            await ReplyAsync(rnd); 
        }
    }


    [Command("coinflip")]
    [Summary("Flip a coin")]
    public async Task Coinflip()
    {
        var coin = new List<string>();
        coin.Add("Heads");
        coin.Add("Tails");
        var rnd = new Random().NextInt64(coin.Count());
        var result = coin[(int)rnd];
        await ReplyAsync(result + "!");
    }

    [Command("Help")]
    [Summary("yea")]
    public async Task Help()
    {
        List<CommandInfo> commands = _commands.Commands.ToList();
        var embedBuilder = new EmbedBuilder();
        foreach (var command in commands)
        {
            var embedFieldText = string.IsNullOrEmpty(command.Summary) ? "No description available\n" : command.Summary;
            embedBuilder.AddField(command.Name, embedFieldText);
        }
        await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
    }
}
