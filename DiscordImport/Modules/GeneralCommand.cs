﻿using System.Globalization;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordImport.Modules;
public class GeneralCommand:ModuleBase<SocketCommandContext>
{
    private readonly DiscordSocketClient _client;
    public GeneralCommand(DiscordSocketClient client)
    {
        _client = client;
    }

    [Command("time")]
    [Summary("Return date, time and timezone.")]
    public async Task Time()
    {
        var localDate = DateTime.Now;
        await ReplyAsync(localDate.ToString(CultureInfo.InvariantCulture) + " " + TimeZoneInfo.Local);
    }

    [Command("date")]
    [Summary("Return date, time and timezone.")]
    public async Task Date()
    {
        var localDate = DateTime.Now;
        await ReplyAsync(localDate.ToString(CultureInfo.InvariantCulture) + " " + TimeZoneInfo.Local);
    }

    [Command("ping")]
    public async Task Ping()
    {
        await ReplyAsync("pong");
    }

    [Command("Import")]
    public async Task Import(string channelId, string destinationId)
    {
        var channel = _client.GetChannel(ulong.Parse(channelId)) as IMessageChannel;
        var destinationChannel = _client.GetChannel(ulong.Parse(destinationId)) as IMessageChannel;
        if (channel != null && destinationChannel != null)
        {
            var messages = await channel.GetMessagesAsync().FlattenAsync();
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.Author.Username}: {message.Content}");
                var attachments = message.Attachments;
                foreach (var a in attachments)
                {
                    Console.WriteLine(a.ProxyUrl);
                    Console.WriteLine(a.Url);
                    await destinationChannel.SendMessageAsync(a.ProxyUrl);
                }
            }
        }
        else
        {
            await ReplyAsync("Channel not found.");
        }
    }
    
    [Command("CheckChannel")]
    public async Task CheckChannel(string channelId)
    {
        var channel = _client.GetChannel(ulong.Parse(channelId)) as IMessageChannel;
        var channelExist = (channel != null);

        switch (channelExist)
        {
            case true:
                await ReplyAsync("Channel Exist");
                break;
            case false:
                await ReplyAsync("Channel not found.");
                break;
            default:
                break;
        }

    }
}