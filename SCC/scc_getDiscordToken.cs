using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

class scc_getDiscordToken
{
    private CommandContext ctx;
    public scc_getDiscordToken(CommandContext ctx)
    {
        this.ctx = ctx;
    }

    internal bool validator(DiscordMessage a)
    {
        if (a.Author.Username == ctx.User.Username && a.Channel == ctx.Channel)
        {
            return true;
        }
        return false;
    }

    public async Task<dynamic> getDiscordTokenFromPMAsync()
    {
        var interactivity = ctx.Client.GetInteractivityModule();
        var DialogQuestion = new DiscordEmbedBuilder
        {
            Title = "I need attention",
            Description = "Zadej tvuj Discord Token.",
            Color = new DiscordColor(255, 0, 0)    
        };

        var message = await ctx.Member.SendMessageAsync(embed: DialogQuestion);
        var userReaction = await interactivity.WaitForMessageAsync(validator);
        if (userReaction != null)
        {
            return userReaction.Message.Content;    
        }
        else
        {
            return null;
        }
    }
}