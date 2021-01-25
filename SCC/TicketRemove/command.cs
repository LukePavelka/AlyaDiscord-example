using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;



namespace AlyaDiscord
{
    public class SCCTicketCommands
    {
        [Command("scc_remove_ticket"), Description("Inicializovat konverzaci pro vyvolání mazacího ticketu."),Hidden]
        public async Task scc_remove_ticket(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            var interactivity = ctx.Client.GetInteractivityModule();
            List<DialogData> Questions = new List<DialogData>();
     
            Questions.Add(new DialogData());

            Questions[0].ChatQuestion = @"Typ operace: vyberte si, napište číslo: 
            [
                    1. **Smazat WS ID** 
                    2. **Smazat celý film**
                    3. **Smazat celý seriál** 
            ]";
            Questions[0].StatusReportDescription = "Typ operace";
            Questions[0].InternalDescription = "";
            Questions[0].AnswerTypeObject = new TicketRemoveMainQuestion(Questions);

            var DialogThread = await dialog.Create(ctx,Questions);
            var Result = await DialogThread.FinalOutputAsync(true);
            
            if (Result[0].AnswerTypeObject.input == 1.ToString())
            {
                var TicketRemoveWsID = new AlyaDiscord.TicketRemove.API.WsId.Remove();
                TicketRemoveWsID.DiscordUuid = ctx.User.Id.ToString();
                TicketRemoveWsID.DiscordUserName = ctx.User.Username;
                TicketRemoveWsID.DiscordMention = ctx.User.Mention;
                TicketRemoveWsID.TimeStampCreation = DateTime.Now.ToString("HH:mm-dd-MM-yyyy");

                TicketRemoveWsID.WebshareIdString = Result[1].AnswerTypeObject.input;
                TicketRemoveWsID.Reason = Result[2].Answer;
                var json = JsonConvert.SerializeObject(TicketRemoveWsID);
                AlyaDiscord.TicketRemove.API.Call.requestwsid.post(json);
                
            }
            else if(Result[0].AnswerTypeObject.input == 2.ToString())
            {
                var TicketRemoveRoot = new AlyaDiscord.TicketRemove.API.Root.Remove();
                TicketRemoveRoot.DiscordUuid = ctx.User.Id.ToString();
                TicketRemoveRoot.DiscordUserName = ctx.User.Username;
                TicketRemoveRoot.DiscordMention = ctx.User.Mention;
                TicketRemoveRoot.TimeStampCreation = DateTime.Now.ToString("HH:mm-dd-MM-yyyy");
   
                if (Result[1].Answer == "csfd")
                {
                    TicketRemoveRoot.ServiceType = 1;
                }
                else if (Result[1].Answer == "tmdb")
                {
                    TicketRemoveRoot.ServiceType = 2;
                }
                else if (Result[1].Answer == "trakt")
                {
                    TicketRemoveRoot.ServiceType = 3;
                }
                else if (Result[1].Answer == "imdb")
                {
                    TicketRemoveRoot.ServiceType = 4;
                }
                else if (Result[1].Answer == "tvdb")
                {
                    TicketRemoveRoot.ServiceType = 5;
                }
                TicketRemoveRoot.ServiceId = Result[2].Answer;
                TicketRemoveRoot.WhatIsTheGoal = "movie";

                TicketRemoveRoot.Reason = Result[3].Answer;
                var json = JsonConvert.SerializeObject(TicketRemoveRoot);
                AlyaDiscord.TicketRemoveAPICall.requestRoot.post(json);
            }
            else if(Result[0].AnswerTypeObject.input == 3.ToString())
            {
                var TicketRemoveRoot = new AlyaDiscord.TicketRemove.API.Root.Remove();
                TicketRemoveRoot.DiscordUuid = ctx.User.Id.ToString();
                TicketRemoveRoot.DiscordUserName = ctx.User.Username;
                TicketRemoveRoot.DiscordMention = ctx.User.Mention;
                TicketRemoveRoot.TimeStampCreation = DateTime.Now.ToString("HH:mm-dd-MM-yyyy");
   
                if (Result[1].Answer == "csfd")
                {
                    TicketRemoveRoot.ServiceType = 1;
                }
                else if (Result[1].Answer == "tmdb")
                {
                    TicketRemoveRoot.ServiceType = 2;
                }
                else if (Result[1].Answer == "trakt")
                {
                    TicketRemoveRoot.ServiceType = 3;
                }
                else if (Result[1].Answer == "imdb")
                {
                    TicketRemoveRoot.ServiceType = 4;
                }
                else if (Result[1].Answer == "tvdb")
                {
                    TicketRemoveRoot.ServiceType = 5;
                }

                TicketRemoveRoot.ServiceId = Result[2].Answer;
                TicketRemoveRoot.WhatIsTheGoal = "tvshow";

                TicketRemoveRoot.Reason = Result[3].Answer;
                var json = JsonConvert.SerializeObject(TicketRemoveRoot);
                AlyaDiscord.TicketRemoveAPICall.requestRoot.post(json);

            }  
        }
        [Command("scc_remove_ticket_ticket"), Description("Vše smazat."),Hidden,RequireOwner]
        public async Task scc_remove_ticket_ticket(CommandContext ctx, string token)
        {
            var stringObjectRoot = AlyaDiscord.TicketRemoveAPICall.requestRoot.getall();
            var stringObjectWsid = AlyaDiscord.TicketRemove.API.Call.requestwsid.getall();

            var objectRoot = AlyaDiscord.TicketRemoveAdminRoot.Object.FromJson(stringObjectRoot);
            var objectWsid = AlyaDiscord.TicketRemoveAdminwsid.Object.FromJson(stringObjectWsid);

            await Task.Run(async () => 
            {
                foreach (var item in objectRoot)
                {
                    await item.proceedAsync(ctx,token);
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }); 

            await Task.Run(async () => 
            {
                foreach (var item in objectWsid)
                {
                    await item.proceedAsync(ctx,token);
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            });
        }
    }

}
