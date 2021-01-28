using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;


namespace AlyaDiscord
{
    public class dialogCommands
    {
        [Command("scc_dialog_alpha"), Description("ziskat ws id ze scc db")]
        public async Task scc_dialog_alpha(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            List<DialogData> Questions = new List<DialogData>();
            Questions.Add(new DialogData());
            Questions[0].ChatQuestion = @"Napište prosím Název **Filmu** nebo **Serialu**, stejně jak by jste to hledaly ve **SCC**";
            Questions[0].StatusReportDescription = "Search Get Info";
            Questions[0].InternalDescription = "SCC_SEARCH_ENTRY";
            Questions[0].AnswerTypeObject = new DATSccSearch(Questions);
            var testovani = await dialog.Create(ctx,Questions);
            var Result = await testovani.FinalOutputAsync(true);
        }
    }
}
