namespace AlyaDiscord
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CideFcObject
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("callsign", NullValueHandling = NullValueHandling.Ignore)]
        public string Callsign { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("ownerCmdr", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerCmdr { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty("nextJump", NullValueHandling = NullValueHandling.Ignore)]
        public string NextJump { get; set; }

        [JsonProperty("nextJumpDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public string NextJumpDateTime { get; set; }

        [JsonProperty("destination", NullValueHandling = NullValueHandling.Ignore)]
        public string Destination { get; set; }

        [JsonProperty("dockingAccess", NullValueHandling = NullValueHandling.Ignore)]
        public string DockingAccess { get; set; }

        [JsonProperty("allowNotorious", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowNotorious { get; set; }

        [JsonProperty("fuelLevel", NullValueHandling = NullValueHandling.Ignore)]
        public long? FuelLevel { get; set; }

        [JsonProperty("jumpRangeCurr", NullValueHandling = NullValueHandling.Ignore)]
        public long? JumpRangeCurr { get; set; }

        [JsonProperty("jumpRangeMax", NullValueHandling = NullValueHandling.Ignore)]
        public long? JumpRangeMax { get; set; }

        [JsonProperty("jumpLeft", NullValueHandling = NullValueHandling.Ignore)]
        public long? JumpLeft { get; set; }

        [JsonProperty("jumpMax", NullValueHandling = NullValueHandling.Ignore)]
        public long? JumpMax { get; set; }

        [JsonProperty("statusMode", NullValueHandling = NullValueHandling.Ignore)]
        public long? StatusMode { get; set; }

        [JsonProperty("jumpDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public string JumpDateTime { get; set; }

        [JsonProperty("fuelInMarket", NullValueHandling = NullValueHandling.Ignore)]
        public long? FuelInMarket { get; set; }

        [JsonProperty("fuelNeeded", NullValueHandling = NullValueHandling.Ignore)]
        public long? FuelNeeded { get; set; }
    }

    public partial class CideFcObject
    {
        public static List<CideFcObject> FromJson(string json) => JsonConvert.DeserializeObject<List<CideFcObject>>(json, AlyaDiscord.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<CideFcObject> self) => JsonConvert.SerializeObject(self, AlyaDiscord.Converter.Settings);
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
