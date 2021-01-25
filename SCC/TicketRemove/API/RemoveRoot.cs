namespace AlyaDiscord.TicketRemove.API.Root
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Remove
    {
        [J("discordUUID", NullValueHandling = N.Ignore)]       public string DiscordUuid { get; set; }      
        [J("discordUserName", NullValueHandling = N.Ignore)]   public string DiscordUserName { get; set; }  
        [J("discordMention", NullValueHandling = N.Ignore)]    public string DiscordMention { get; set; }   
        [J("timeStampCreation", NullValueHandling = N.Ignore)] public string TimeStampCreation { get; set; }
        [J("serviceType", NullValueHandling = N.Ignore)]       public long? ServiceType { get; set; }       
        [J("serviceID", NullValueHandling = N.Ignore)]         public string ServiceId { get; set; }        
        [J("reason", NullValueHandling = N.Ignore)]            public string Reason { get; set; }           
        [J("whatIsTheGoal", NullValueHandling = N.Ignore)]     public string WhatIsTheGoal { get; set; }    
    }

    public partial class Remove
    {
        public static Remove FromJson(string json) => JsonConvert.DeserializeObject<Remove>(json, AlyaDiscord.TicketRemove.API.Root.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Remove self) => JsonConvert.SerializeObject(self, AlyaDiscord.TicketRemove.API.Root.Converter.Settings);
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
