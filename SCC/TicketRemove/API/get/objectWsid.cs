namespace AlyaDiscord.TicketRemoveAdminwsid
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;
    using DSharpPlus.Entities;
    using System.Text.RegularExpressions;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class Object
    {
        [J("id", NullValueHandling = N.Ignore)]                public string Id { get; set; }               
        [J("webshareIDString", NullValueHandling = N.Ignore)]  public string WebshareIdString { get; set; } 
        [J("reason", NullValueHandling = N.Ignore)]            public string Reason { get; set; }           
        [J("whatIsTheGoal")]                                   public dynamic WhatIsTheGoal { get; set; }   
        [J("discordUUID", NullValueHandling = N.Ignore)]       public string DiscordUuid { get; set; }      
        [J("discordUserName", NullValueHandling = N.Ignore)]   public string DiscordUserName { get; set; }  
        [J("discordMention", NullValueHandling = N.Ignore)]    public string DiscordMention { get; set; }   
        [J("timeStampCreation", NullValueHandling = N.Ignore)] public string TimeStampCreation { get; set; }

        public async Task<dynamic> TryRemoveAsync(string token,string wsid)
        {
            var scc = new SCC(token);
            var ResultFromSccAPIS = await scc.deleteStreamAsync(wsid);
            return ResultFromSccAPIS;
        }

        public void RemoveFromDB()
        {
            AlyaDiscord.TicketRemoveAPICall.requestRoot.delete(Id);
        }
        public List<string> GetList()
        {
            List<string> outputvar;
            string[] lines = WebshareIdString.Split(new[] { "\r\n", "\r", "\n", " " },StringSplitOptions.None);
            lines.ToList();
            outputvar = new List<string>();
            Regex r = new Regex(@"(?<=file/)[a-zA-Z0-9]+", RegexOptions.Compiled);
            foreach (var item in lines)
            {
                if (item.Contains("/"))
                {
                    Match match = r.Match(item);
                    outputvar.Add(match.Value);
                }
                else
                {
                    outputvar.Add(item);
                }
                    
            }
            return outputvar;
           
        }


        public async System.Threading.Tasks.Task FeedbackToAdminAsync(DSharpPlus.CommandsNext.CommandContext ctx, DiscordEmbedBuilder emb)
        {
            emb.AddField("Žadatel",DiscordUserName);
            //hidden, contains sensitive data
            var guild = await ctx.Client.GetGuildAsync(000000000000000000);
            //hidden, contains sensitive data
            var personArwwarr = await guild.GetMemberAsync(000000000000000000);
            await personArwwarr.SendMessageAsync(embed: emb);
        }

        private DiscordEmbedBuilder CreateBaseDiscordEmbed()
        {
            var BaseRespond = new DiscordEmbedBuilder
            {
                Title = "Ticket Remove - User Report",
                Description = "Oznámení pro správce db SCC, informace ohledně ticketu.",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "Report vytvořen"
                },
                Color = new DiscordColor(25, 118, 210),
            };
            BaseRespond.AddField("Důvod vymazání", Reason, false);
            BaseRespond.WithTimestamp(DateTime.Now);
            return BaseRespond;
         
        }

        public async System.Threading.Tasks.Task FeedbackToUserAsync(DSharpPlus.CommandsNext.CommandContext ctx, DiscordEmbedBuilder emb)
        {
            var guild = await ctx.Client.GetGuildAsync(706602726565609563);
            var person = await guild.GetMemberAsync(Convert.ToUInt64(DiscordUuid));
            await person.SendMessageAsync(embed: emb);
        }

        public async System.Threading.Tasks.Task proceedAsync(DSharpPlus.CommandsNext.CommandContext ctx, string token)
        {
            int DoneItems = 0;
            var UserMessage = CreateBaseDiscordEmbed();
            var AdminMessage = CreateBaseDiscordEmbed();
            AdminMessage.Title = "Ticket Remove - Admin Report";
            AdminMessage.Description = "Oznámení pro adminy SCC, informace ohledně ticketu.";
            UserMessage.Color = new DiscordColor(84, 227, 70);
            AdminMessage.Color = new DiscordColor(84, 227, 70);

            foreach (var item in GetList())
            {
                var Result = await TryRemoveAsync(token,item);
                if (Result[0] == "1")
                {
                    UserMessage.AddField("WS ID", item, true);
                    AdminMessage.AddField("WS ID", item, true);
                    UserMessage.AddField("Status", "Smazáno :white_check_mark:", false);
                    AdminMessage.AddField("Status", "Smazáno :white_check_mark:", false);
                    DoneItems = DoneItems + 1;
                }
                else
                {
                    UserMessage.AddField("WS ID", item, true);
                    AdminMessage.AddField("WS ID", item, true);
                    UserMessage.AddField("Status", "Error :red_square:", false);
                    AdminMessage.AddField("Status", "Error :red_square:", false);
                    UserMessage.Color = new DiscordColor(255, 0, 0);
                    AdminMessage.Color = new DiscordColor(255, 0, 0);
                    DoneItems = DoneItems + 1;
                }
                if (DoneItems == 5)
                {
                    await FeedbackToUserAsync(ctx,UserMessage);
                    await FeedbackToAdminAsync(ctx,UserMessage);
                    UserMessage = CreateBaseDiscordEmbed();
                    AdminMessage = CreateBaseDiscordEmbed();
                    UserMessage.Color = new DiscordColor(84, 227, 70);
                    AdminMessage.Color = new DiscordColor(84, 227, 70);
                    DoneItems = 0;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            if (DoneItems > 0)
            {
                await FeedbackToUserAsync(ctx,UserMessage);
                await FeedbackToAdminAsync(ctx,UserMessage);
            }
            AlyaDiscord.TicketRemove.API.Call.requestwsid.delete(Id);
        }
    }


    public partial class Object
    {
        public static List<Object> FromJson(string json) => JsonConvert.DeserializeObject<List<Object>>(json, AlyaDiscord.TicketRemoveAdminwsid.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Object> self) => JsonConvert.SerializeObject(self, AlyaDiscord.TicketRemoveAdminwsid.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
