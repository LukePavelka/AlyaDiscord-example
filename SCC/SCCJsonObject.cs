namespace AlyaBackend
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class SccParentObject
    {
        [J("totalCount", NullValueHandling = N.Ignore)] public long? TotalCount { get; set; }           
        [J("data", NullValueHandling = N.Ignore)]       public SccParentObjectDatum[] Data { get; set; }
    }

    public partial class SccParentObjectDatum
    {
        [J("_id", NullValueHandling = N.Ignore)]     public string Id { get; set; }          
        [J("_score")]                                public dynamic Score { get; set; }      
        [J("_source", NullValueHandling = N.Ignore)] public PurpleSource Source { get; set; }
    }

    public partial class PurpleSource
    {
        [J("info_labels", NullValueHandling = N.Ignore)]       public PurpleInfoLabels InfoLabels { get; set; }         
        [J("cast", NullValueHandling = N.Ignore)]              public PurpleCast[] Cast { get; set; }                   
        [J("services", NullValueHandling = N.Ignore)]          public PurpleServices Services { get; set; }             
        [J("i18n_info_labels", NullValueHandling = N.Ignore)]  public PurpleI18NInfoLabel[] I18NInfoLabels { get; set; }
        [J("play_count", NullValueHandling = N.Ignore)]        public long? PlayCount { get; set; }                     
        [J("available_streams", NullValueHandling = N.Ignore)] public AvailableStreams AvailableStreams { get; set; }   
        [J("root_parent", NullValueHandling = N.Ignore)]       public string RootParent { get; set; }                   
        [J("children_count", NullValueHandling = N.Ignore)]    public long? ChildrenCount { get; set; }                 
        [J("stream_info", NullValueHandling = N.Ignore)]       public StreamInfo StreamInfo { get; set; }               
    }

    public partial class AvailableStreams
    {
        [J("count", NullValueHandling = N.Ignore)]               public long? Count { get; set; }                   
        [J("audio_languages", NullValueHandling = N.Ignore)]     public AudioLanguage[] AudioLanguages { get; set; }
        [J("subtitles_languages", NullValueHandling = N.Ignore)] public string[] SubtitlesLanguages { get; set; }   
    }

    public partial class AudioLanguage
    {
        [J("lang", NullValueHandling = N.Ignore)]       public string Lang { get; set; }     
        [J("date_added", NullValueHandling = N.Ignore)] public string DateAdded { get; set; }
    }

    public partial class PurpleCast
    {
        [J("name", NullValueHandling = N.Ignore)]      public string Name { get; set; }  
        [J("role", NullValueHandling = N.Ignore)]      public string Role { get; set; }  
        [J("thumbnail", NullValueHandling = N.Ignore)] public Uri Thumbnail { get; set; }
    }

    public partial class PurpleI18NInfoLabel
    {
        [J("lang", NullValueHandling = N.Ignore)]          public string Lang { get; set; }          
        [J("title", NullValueHandling = N.Ignore)]         public string Title { get; set; }         
        [J("plot", NullValueHandling = N.Ignore)]          public string Plot { get; set; }          
        [J("rating", NullValueHandling = N.Ignore)]        public double? Rating { get; set; }       
        [J("votes", NullValueHandling = N.Ignore)]         public string Votes { get; set; }         
        [J("art", NullValueHandling = N.Ignore)]           public PurpleArt Art { get; set; }        
        [J("parent_titles", NullValueHandling = N.Ignore)] public string[] ParentTitles { get; set; }
        [J("trailer", NullValueHandling = N.Ignore)]       public string Trailer { get; set; }       
    }

    public partial class PurpleArt
    {
        [J("poster")] public Uri Poster { get; set; }
    }

    public partial class PurpleInfoLabels
    {
        [J("originaltitle", NullValueHandling = N.Ignore)] public string Originaltitle { get; set; }
        [J("genre", NullValueHandling = N.Ignore)]         public string[] Genre { get; set; }      
        [J("year", NullValueHandling = N.Ignore)]          public long? Year { get; set; }          
        [J("duration", NullValueHandling = N.Ignore)]      public long? Duration { get; set; }      
        [J("director", NullValueHandling = N.Ignore)]      public string[] Director { get; set; }   
        [J("studio", NullValueHandling = N.Ignore)]        public dynamic[] Studio { get; set; }    
        [J("writer", NullValueHandling = N.Ignore)]        public string[] Writer { get; set; }     
        [J("aired", NullValueHandling = N.Ignore)]         public string Aired { get; set; }        
        [J("dateadded", NullValueHandling = N.Ignore)]     public string Dateadded { get; set; }    
        [J("mediatype", NullValueHandling = N.Ignore)]     public string Mediatype { get; set; }    
        [J("season", NullValueHandling = N.Ignore)]        public long? Season { get; set; }        
        [J("episode", NullValueHandling = N.Ignore)]       public long? Episode { get; set; }       
        [J("premiered", NullValueHandling = N.Ignore)]     public string Premiered { get; set; }    
    }

    public partial class PurpleServices
    {
        [J("csfd", NullValueHandling = N.Ignore)]            public string Csfd { get; set; }         
        [J("trakt", NullValueHandling = N.Ignore)]           public string Trakt { get; set; }        
        [J("tmdb", NullValueHandling = N.Ignore)]            public string Tmdb { get; set; }         
        [J("tvdb", NullValueHandling = N.Ignore)]            public string Tvdb { get; set; }         
        [J("trakt_with_type", NullValueHandling = N.Ignore)] public string TraktWithType { get; set; }
        [J("imdb", NullValueHandling = N.Ignore)]            public string Imdb { get; set; }         
    }

    public partial class StreamInfo
    {
        [J("audio", NullValueHandling = N.Ignore)] public StreamInfoAudio Audio { get; set; }
        [J("video", NullValueHandling = N.Ignore)] public StreamInfoVideo Video { get; set; }
        [J("subtitles")]                           public Subtitles Subtitles { get; set; }  
    }

    public partial class StreamInfoAudio
    {
        [J("language", NullValueHandling = N.Ignore)] public string Language { get; set; }
        [J("codec", NullValueHandling = N.Ignore)]    public string Codec { get; set; }   
        [J("channels", NullValueHandling = N.Ignore)] public long? Channels { get; set; } 
    }

    public partial class Subtitles
    {
        [J("language", NullValueHandling = N.Ignore)] public string Language { get; set; }
    }

    public partial class StreamInfoVideo
    {
        [J("width", NullValueHandling = N.Ignore)]    public long? Width { get; set; }     
        [J("height", NullValueHandling = N.Ignore)]   public long? Height { get; set; }    
        [J("codec", NullValueHandling = N.Ignore)]    public string Codec { get; set; }    
        [J("aspect", NullValueHandling = N.Ignore)]   public double? Aspect { get; set; }  
        [J("duration", NullValueHandling = N.Ignore)] public double? Duration { get; set; }
    }

    public partial class SccSearchObject
    {
        [J("totalCount", NullValueHandling = N.Ignore)] public long? TotalCount { get; set; }           
        [J("data", NullValueHandling = N.Ignore)]       public SccSearchObjectDatum[] Data { get; set; }
        [J("pagination", NullValueHandling = N.Ignore)] public Pagination Pagination { get; set; }      
    }

    public partial class SccSearchObjectDatum
    {
        [J("_id", NullValueHandling = N.Ignore)]     public string Id { get; set; }          
        [J("_score", NullValueHandling = N.Ignore)]  public double? Score { get; set; }      
        [J("_source", NullValueHandling = N.Ignore)] public FluffySource Source { get; set; }
    }

    public partial class FluffySource
    {
        [J("info_labels", NullValueHandling = N.Ignore)]       public FluffyInfoLabels InfoLabels { get; set; }         
        [J("cast", NullValueHandling = N.Ignore)]              public FluffyCast[] Cast { get; set; }                   
        [J("services", NullValueHandling = N.Ignore)]          public FluffyServices Services { get; set; }             
        [J("i18n_info_labels", NullValueHandling = N.Ignore)]  public FluffyI18NInfoLabel[] I18NInfoLabels { get; set; }
        [J("play_count", NullValueHandling = N.Ignore)]        public long? PlayCount { get; set; }                     
        [J("budget", NullValueHandling = N.Ignore)]            public long? Budget { get; set; }                        
        [J("revenue", NullValueHandling = N.Ignore)]           public long? Revenue { get; set; }                       
        [J("language", NullValueHandling = N.Ignore)]          public string Language { get; set; }                     
        [J("country", NullValueHandling = N.Ignore)]           public string Country { get; set; }                      
        [J("adult", NullValueHandling = N.Ignore)]             public bool? Adult { get; set; }                         
        [J("available_streams", NullValueHandling = N.Ignore)] public AvailableStreams AvailableStreams { get; set; }   
        [J("children_count", NullValueHandling = N.Ignore)]    public long? ChildrenCount { get; set; }                 
        [J("stream_info", NullValueHandling = N.Ignore)]       public StreamInfo StreamInfo { get; set; }               
        [J("root_parent", NullValueHandling = N.Ignore)]       public string RootParent { get; set; }                   
    }

    public partial class FluffyCast
    {
        [J("name", NullValueHandling = N.Ignore)]      public string Name { get; set; }     
        [J("role", NullValueHandling = N.Ignore)]      public string Role { get; set; }     
        [J("thumbnail", NullValueHandling = N.Ignore)] public string Thumbnail { get; set; }
    }

    public partial class FluffyI18NInfoLabel
    {
        [J("lang", NullValueHandling = N.Ignore)]          public string Lang { get; set; }           
        [J("title", NullValueHandling = N.Ignore)]         public string Title { get; set; }          
        [J("plot", NullValueHandling = N.Ignore)]          public string Plot { get; set; }           
        [J("plotoutline", NullValueHandling = N.Ignore)]   public string Plotoutline { get; set; }    
        [J("trailer", NullValueHandling = N.Ignore)]       public string Trailer { get; set; }        
        [J("rating", NullValueHandling = N.Ignore)]        public double? Rating { get; set; }        
        [J("votes", NullValueHandling = N.Ignore)]         public string Votes { get; set; }          
        [J("art", NullValueHandling = N.Ignore)]           public FluffyArt Art { get; set; }         
        [J("parent_titles", NullValueHandling = N.Ignore)] public dynamic[] ParentTitles { get; set; }
    }

    public partial class FluffyArt
    {
        [J("poster")]    public string Poster { get; set; }    
        [J("fanart")]    public Uri Fanart { get; set; }       
        [J("thumb")]     public Uri Thumb { get; set; }        
        [J("banner")]    public Uri Banner { get; set; }       
        [J("clearart")]  public Uri Clearart { get; set; }     
        [J("clearlogo")] public Uri Clearlogo { get; set; }    
        [J("landscape")] public dynamic Landscape { get; set; }
        [J("icon")]      public dynamic Icon { get; set; }     
    }

    public partial class FluffyInfoLabels
    {
        [J("originaltitle", NullValueHandling = N.Ignore)] public string Originaltitle { get; set; }
        [J("genre", NullValueHandling = N.Ignore)]         public string[] Genre { get; set; }      
        [J("year", NullValueHandling = N.Ignore)]          public long? Year { get; set; }          
        [J("duration", NullValueHandling = N.Ignore)]      public long? Duration { get; set; }      
        [J("director", NullValueHandling = N.Ignore)]      public string[] Director { get; set; }   
        [J("mpaa", NullValueHandling = N.Ignore)]          public string Mpaa { get; set; }         
        [J("studio", NullValueHandling = N.Ignore)]        public string[] Studio { get; set; }     
        [J("writer", NullValueHandling = N.Ignore)]        public string[] Writer { get; set; }     
        [J("premiered", NullValueHandling = N.Ignore)]     public string Premiered { get; set; }    
        [J("trailer", NullValueHandling = N.Ignore)]       public string Trailer { get; set; }      
        [J("dateadded", NullValueHandling = N.Ignore)]     public string Dateadded { get; set; }    
        [J("mediatype", NullValueHandling = N.Ignore)]     public string Mediatype { get; set; }    
        [J("season", NullValueHandling = N.Ignore)]        public long? Season { get; set; }        
        [J("episode", NullValueHandling = N.Ignore)]       public long? Episode { get; set; }       
        [J("aired", NullValueHandling = N.Ignore)]         public string Aired { get; set; }        
    }

    public partial class FluffyServices
    {
        [J("csfd", NullValueHandling = N.Ignore)]            public string Csfd { get; set; }         
        [J("trakt", NullValueHandling = N.Ignore)]           public string Trakt { get; set; }        
        [J("slug", NullValueHandling = N.Ignore)]            public string Slug { get; set; }         
        [J("tmdb", NullValueHandling = N.Ignore)]            public string Tmdb { get; set; }         
        [J("imdb", NullValueHandling = N.Ignore)]            public string Imdb { get; set; }         
        [J("trakt_with_type", NullValueHandling = N.Ignore)] public string TraktWithType { get; set; }
        [J("tvdb", NullValueHandling = N.Ignore)]            public string Tvdb { get; set; }         
        [J("fanart", NullValueHandling = N.Ignore)]          public string Fanart { get; set; }       
    }

    public partial class Pagination
    {
        [J("next", NullValueHandling = N.Ignore)]      public string Next { get; set; }    
        [J("prev", NullValueHandling = N.Ignore)]      public string Prev { get; set; }    
        [J("page", NullValueHandling = N.Ignore)]      public long? Page { get; set; }     
        [J("pageCount", NullValueHandling = N.Ignore)] public long? PageCount { get; set; }
        [J("from", NullValueHandling = N.Ignore)]      public long? From { get; set; }     
        [J("limit", NullValueHandling = N.Ignore)]     public long? Limit { get; set; }    
    }

    public partial class SccStreamObject
    {
        [J("_id", NullValueHandling = N.Ignore)]       public string Id { get; set; }           
        [J("name", NullValueHandling = N.Ignore)]      public string Name { get; set; }         
        [J("provider", NullValueHandling = N.Ignore)]  public string Provider { get; set; }     
        [J("ident", NullValueHandling = N.Ignore)]     public string Ident { get; set; }        
        [J("size", NullValueHandling = N.Ignore)]      public long? Size { get; set; }          
        [J("validated", NullValueHandling = N.Ignore)] public bool? Validated { get; set; }     
        [J("audio", NullValueHandling = N.Ignore)]     public AudioElement[] Audio { get; set; }
        [J("video", NullValueHandling = N.Ignore)]     public VideoElement[] Video { get; set; }
        [J("subtitles", NullValueHandling = N.Ignore)] public Subtitle[] Subtitles { get; set; }
    }

    public partial class AudioElement
    {
        [J("_id", NullValueHandling = N.Ignore)]      public string Id { get; set; }      
        [J("language", NullValueHandling = N.Ignore)] public string Language { get; set; }
        [J("codec", NullValueHandling = N.Ignore)]    public string Codec { get; set; }   
        [J("channels", NullValueHandling = N.Ignore)] public long? Channels { get; set; } 
    }

    public partial class Subtitle
    {
        [J("_id", NullValueHandling = N.Ignore)]      public string Id { get; set; }      
        [J("language", NullValueHandling = N.Ignore)] public string Language { get; set; }
        [J("forced", NullValueHandling = N.Ignore)]   public bool? Forced { get; set; }   
    }

    public partial class VideoElement
    {
        [J("_id", NullValueHandling = N.Ignore)]      public string Id { get; set; }       
        [J("width", NullValueHandling = N.Ignore)]    public long? Width { get; set; }     
        [J("height", NullValueHandling = N.Ignore)]   public long? Height { get; set; }    
        [J("codec", NullValueHandling = N.Ignore)]    public string Codec { get; set; }    
        [J("aspect", NullValueHandling = N.Ignore)]   public double? Aspect { get; set; }  
        [J("hdr", NullValueHandling = N.Ignore)]      public bool? Hdr { get; set; }       
        [J("3d", NullValueHandling = N.Ignore)]       public bool? The3D { get; set; }     
        [J("duration", NullValueHandling = N.Ignore)] public double? Duration { get; set; }
    }

    public partial class SccParentObject
    {
        public static SccParentObject FromJson(string json) => JsonConvert.DeserializeObject<SccParentObject>(json, AlyaBackend.Converter.Settings);
    }

    public partial class SccSearchObject
    {
        public static SccSearchObject FromJson(string json) => JsonConvert.DeserializeObject<SccSearchObject>(json, AlyaBackend.Converter.Settings);
    }

    public partial class SccStreamObject
    {
        public static SccStreamObject[] FromJson(string json) => JsonConvert.DeserializeObject<SccStreamObject[]>(json, AlyaBackend.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SccParentObject self) => JsonConvert.SerializeObject(self, AlyaBackend.Converter.Settings);
        public static string ToJson(this SccSearchObject self) => JsonConvert.SerializeObject(self, AlyaBackend.Converter.Settings);
        public static string ToJson(this SccStreamObject[] self) => JsonConvert.SerializeObject(self, AlyaBackend.Converter.Settings);
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
