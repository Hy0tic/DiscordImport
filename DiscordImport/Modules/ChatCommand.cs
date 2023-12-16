
using Discord.Commands;
using Discord.WebSocket;
using ChatGptNet;
using ChatGptNet.Extensions;

namespace DiscordImport.Modules;

public class ChatCommand : ModuleBase<SocketCommandContext>
{
    private readonly DiscordSocketClient _client;
    private IChatGptClient _chatGptClient;

    public ChatCommand(DiscordSocketClient client, IChatGptClient chatGptClient)
    {
        _client = client;
        _chatGptClient = chatGptClient;
    }

    [Command("gpt")]
    [Summary("")]
    public async Task Gpt()
    {
        var response = await _chatGptClient.AskAsync("how was the weather today");
        await ReplyAsync(response.GetContent());
    }
}