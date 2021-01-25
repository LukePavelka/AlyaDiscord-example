namespace AlyaDiscord.TicketRemoveAdminRoot
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;
    using DSharpPlus.Interactivity;
    using DSharpPlus.Entities;
    using DSharpPlus.CommandsNext;

    public partial class Object
    {
        [J("id", NullValueHandling = N.Ignore)]                public string Id { get; set; }               
        [J("serviceType", NullValueHandling = N.Ignore)]       public long? ServiceType { get; set; }       
        [J("serviceID", NullValueHandling = N.Ignore)]         public string ServiceId { get; set; }        
        [J("reason", NullValueHandling = N.Ignore)]            public string Reason { get; set; }           
        [J("whatIsTheGoal", NullValueHandling = N.Ignore)]     public string WhatIsTheGoal { get; set; }    
        [J("discordUUID", NullValueHandling = N.Ignore)]       public string DiscordUuid { get; set; }      
        [J("discordUserName", NullValueHandling = N.Ignore)]   public string DiscordUserName { get; set; }  
        [J("discordMention", NullValueHandling = N.Ignore)]    public string DiscordMention { get; set; }   
        [J("timeStampCreation", NullValueHandling = N.Ignore)] public string TimeStampCreation { get; set; }

        public async System.Threading.Tasks.Task<dynamic> TryRemoveAsync(string token)
        {
            var scc = new SCC(token);
            var ResultFromSccAPIS = await scc.deleteMediaAsync(WhatIsTheGoal,TheServiceType(),ServiceId);
            return ResultFromSccAPIS;
        }

        public void RemoveFromDB()
        {
            AlyaDiscord.TicketRemoveAPICall.requestRoot.delete(Id);
        }
        public string TheServiceType()
        {
            if (ServiceType == 1)
            {
                return "csfd";
            }
            else if (ServiceType == 2)
            {
                return "tmdb";
            }
            else if (ServiceType == 3)
            {
                return "trakt";
            }
            else if (ServiceType == 4)
            {
                return "imdb";
            }
            else if (ServiceType == 5)
            {
                return "tvdb";
            }
            return "error";
        }

        public async System.Threading.Tasks.Task FeedbackToAdminAsync(DSharpPlus.CommandsNext.CommandContext ctx, DiscordEmbedBuilder emb)
        {
            emb.AddField("Žadatel",DiscordUserName);
            //hidden, contains sensitive data
            var guild = await ctx.Client.GetGuildAsync(00000000000000000);
            //hidden, contains sensitive data
            var personArwwarr = await guild.GetMemberAsync(0000000000000000);
            await personArwwarr.SendMessageAsync(embed: emb);
        }

        private DiscordEmbedBuilder CreateBaseDiscordEmbed()
        {
            var BaseRespond = new DiscordEmbedBuilder
            {
                Title = "Ticket Remove - User Report",
                Description = "Oznamení pro správce db SCC, informace ohledně ticketu.",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "Report vytvořen"
                },
                Color = new DiscordColor(25, 118, 210),
            };
            BaseRespond.AddField(TheServiceType(), ServiceId, true);
            BaseRespond.AddField("Důvod vymazání", Reason, true);
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
            var UserMessage = CreateBaseDiscordEmbed();
            var AdminMessage = CreateBaseDiscordEmbed();
            AdminMessage.Title = "Ticket Remove - Admin Report";
            AdminMessage.Description = "Oznámení pro adminy SCC, informace ohledně ticketu.";
            var Result = await TryRemoveAsync(token);
            if (Result[0] == "1")
            {
                UserMessage.AddField("Status", "Smazáno :white_check_mark:", false);
                AdminMessage.AddField("Status", "Smazáno :white_check_mark:", false);
                UserMessage.Color = new DiscordColor(84, 227, 70);
                AdminMessage.Color = new DiscordColor(84, 227, 70);
            }
            else
            {
                UserMessage.AddField("Status", "Error :red_square:", false);
                AdminMessage.AddField("Status", "Error :red_square:", false);
                UserMessage.Color = new DiscordColor(255, 0, 0);
                AdminMessage.Color = new DiscordColor(255, 0, 0);
            }
            await FeedbackToUserAsync(ctx,UserMessage);
            await FeedbackToAdminAsync(ctx,UserMessage);
            AlyaDiscord.TicketRemoveAPICall.requestRoot.delete(Id);
        }
    }

    public partial class Object
    {
        public static List<Object> FromJson(string json) => JsonConvert.DeserializeObject<List<Object>>(json, AlyaDiscord.TicketRemoveAdminRoot.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Object> self) => JsonConvert.SerializeObject(self, AlyaDiscord.TicketRemoveAdminRoot.Converter.Settings);
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
