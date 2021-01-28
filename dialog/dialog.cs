using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;


namespace AlyaDiscord
{
    public class dialog
    {
        private CommandContext ctx;
        private List<DialogData> dialogData;
        private DiscordMessage StatusReportMessageID;
        private DiscordMessage DialogMessageID;

        public dialog(CommandContext CTX, List<DialogData> DATA)
        {
            ctx = CTX;
            dialogData = DATA;
        }


        public static async Task<dialog> Create(CommandContext ss, List<DialogData> sdsd)
        {
            var dialogo = new dialog(ss,sdsd);
            await dialogo.Initialize();
            return dialogo;
        }

        private async Task Initialize()
        {
            await StatusReportFirst();
            await konverzace();
        }
   
        private async Task StatusReportFirst()
        {
            string DescriptionFunc = $"{ctx.User.Mention}\n";

            foreach (var item in dialogData)
            {
                DescriptionFunc += String.Format($"{item.emojiidk} **{item.StatusReportDescription}:** {item.Answer}\n");
            }

            var TemplateChannelNoneStatus = new DiscordEmbedBuilder
            {
                Title = "Status Report",
                Description = DescriptionFunc,
                Color = new DiscordColor(255, 0, 0)    
            };
    
            StatusReportMessageID = await ctx.RespondAsync(embed: TemplateChannelNoneStatus);
            DialogMessageID = await ctx.RespondAsync(content: "Starting");

        }

        private async Task StatusReportApplyChange()
        {
            string DescriptionFunc = $"{ctx.User.Mention}\n";
            int answerCounter = 0;
            float r = 1;
            float g = 1;
            float b = 1;

            foreach (var item in dialogData)
            {
                if (item.Answer != null)
                {
                    item.emojiidk = item.emojigood;
                    answerCounter= answerCounter +1;
                }
                DescriptionFunc += String.Format($"{item.emojiidk} **{item.StatusReportDescription}:** {item.Answer}\n");
            }

            var TemplateChannelNoneStatus = new DiscordEmbedBuilder
            {
                Title = "Status Report",
                Description = DescriptionFunc
            };

            r = (float)answerCounter/(float)dialogData.Count() * ((float)180/(float)255);
            g = ((float)1 - (float)r);
            b = (float)0;
            TemplateChannelNoneStatus.Color = new DiscordColor(g,r,b);
            await StatusReportMessageID.ModifyAsync(embed: TemplateChannelNoneStatus);
        }

        private bool AnswerGrapper(DiscordMessage msg)
        {
            if (msg.Author.Username == ctx.User.Username && msg.Channel == ctx.Channel)
            {
                return true;   
            }
            else
            {
                return false;
            }
        }

        private async Task konverzace()
        {
            var interactivity = ctx.Client.GetInteractivityModule();
            for (int i = 0; i < dialogData.Count; i++)
            {
                var DialogQuestion = new DiscordEmbedBuilder
                {
                    Title = "I need attention",
                    Color = new DiscordColor(255, 0, 0)    
                };

                if (dialogData[i].ChatQuestion.Length < 1999)
                {
                    DialogQuestion.Description = dialogData[i].ChatQuestion;
                    await DialogMessageID.ModifyAsync(embed:DialogQuestion,content: "");   
                }
                else if(dialogData[i].ChatQuestion.Length > 1999)
                {
                    var PageObj = interactivity.GeneratePagesInEmbeds(dialogData[i].ChatQuestion);
                    await interactivity.SendPaginatedMessage(ctx.Channel,ctx.User,PageObj);
                    Console.WriteLine("sdsd");
                }

                var userAnswer = await interactivity.WaitForMessageAsync(AnswerGrapper);
                //null check if null == no reaction >> maybe user not respond, delete board, throw message about too much waiting and END
                if (userAnswer == null)
                {
                    DialogQuestion.Title = "END: Failed";
                    DialogQuestion.Description = $"**Interaktivní Dialog** byl **ukončen** z **duvodu absence odpovědí** od uživatele {ctx.User.Mention}.";
                    await StatusReportMessageID.DeleteAsync();
                    await DialogMessageID.ModifyAsync(embed: DialogQuestion);
                    throw new System.ArgumentException("None input from user", "userAnswer");
                    
                }
                dialogData[i].Answer = userAnswer.Message.Content;
                if (dialogData[i].AnswerTypeObject != null)
                {
                    dialogData[i].AnswerTypeObject.ctx = ctx;
                    dialogData[i].AnswerTypeObject.DialogMessageID = DialogMessageID;
                    dialogData[i].AnswerTypeObject.StatusReportMessageID = StatusReportMessageID;
                    dialogData[i].AnswerTypeObject.index = i;
                    dialogData[i].AnswerTypeObject.input = userAnswer.Message.Content;
                    dialogData[i].Answer = await dialogData[i].AnswerTypeObject.output(); 
                }

                await userAnswer.Message.DeleteAsync();
                await StatusReportApplyChange();
            }
            await StatusReportApplyChange();
        }

        public dynamic FinalGetDialogMessageID()
        {
            return DialogMessageID;
        }
        public dynamic FinalGetStatusReportMessageID()
        {
            return StatusReportMessageID;
        }

        public async Task<dynamic> FinalOutputAsync(bool del)
        {
            if (del == true)
            {
                await DialogMessageID.DeleteAsync();   
            }
            return dialogData;
        }
    
    }

}
