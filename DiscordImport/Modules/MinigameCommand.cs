using Discord;
using Discord.Commands;

namespace DiscordImport.Modules;
public class Minigame:ModuleBase<SocketCommandContext>
{
    [Command("rps")]
    [Summary("Rock paper scissor, do -rps {your choice here}. Ex(choosing scissor): -rps s")]
    public async Task RockPaperScissor(string input)
    {   
        var inputemote = new Emoji("\uD83D\uDC4C");
        switch (input.ToLower())
        {
            case "r":
                inputemote = Emoji.Parse(":gem:");
                break;
            case "p":
                inputemote = Emoji.Parse(":newspaper2:");
                break;
            case "s":
                inputemote = Emoji.Parse(":scissors:");
                break;
        }

        var rnd = new Random();
        var choices = new List<string>() {"r","p","s"};
        var randIndex = rnd.Next(choices.Count());
        var botchoice = choices[randIndex];
        var botchoiceemote = new Emoji("\uD83D\uDC4C");
        if (botchoice != "r")
        {
            botchoiceemote = botchoice switch
            {
                "p" => Emoji.Parse(":newspaper2:"),
                "s" => Emoji.Parse(":scissors:"),
                _ => botchoiceemote
            };
        }
        else
        {
            botchoiceemote = Emoji.Parse(":gem:");
        }

        if(input == botchoice)
        {
            await ReplyAsync(Context.User.Mention+" " + inputemote.ToString() + " vs " + botchoiceemote + " Tie");
        }

        if((input == "r" && botchoice == "s") || (input == "s" && botchoice == "p") || (input == "p" && botchoice == "r"))
        {
            await ReplyAsync(Context.User.Mention+" " + inputemote.ToString() + " vs " + botchoiceemote + " You Won!");
        }
        if((input == "p" && botchoice == "s") || (input == "r" && botchoice == "p") || (input == "s" && botchoice == "r"))
        {
            await ReplyAsync(Context.User.Mention+" " + inputemote.ToString() + " vs " + botchoiceemote + " You Lost!");
        }
    }
}