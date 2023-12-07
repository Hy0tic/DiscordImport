using System.Globalization;
using Discord.Commands;

namespace DiscordImport.Modules;
public class ClockCommands:ModuleBase<SocketCommandContext>
{
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


}