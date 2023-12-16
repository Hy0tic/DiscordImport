
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordImport.Modules;

public class ChatCommand : ModuleBase<SocketCommandContext>
{
    private readonly DiscordSocketClient _client;

    public ChatCommand(DiscordSocketClient client)
    {
        _client = client;
    }
}