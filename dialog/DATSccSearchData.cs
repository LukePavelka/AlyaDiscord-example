using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlyaDiscord
{
    public class DATSccSearchData
    {
        public int index {get;set;}
        public string name {get;set;}
        public string type {get;set;}
        public string year {get;set;}
        public string sccid {get;set;}
        public int IndexSeason {get;set;}
        public int IndexEpisode {get;set;}
        public List<string> streams { get; set; }
        public string csfd {get;set;}
        public string tmdb {get;set;}
        public string imdb {get;set;}
        public string trakt {get;set;}
        public string tvdb {get;set;}


        public async Task getStreamsAsync()
        {
            streams = new List<string>();
            if (sccid != null)
            {
                var resultFromApi = await SCC.GetInfoStreamAsync(sccid);
                var sccStreamObject = AlyaBackend.SccStreamObject.FromJson(resultFromApi);
                if (sccStreamObject != null)
                {
                    foreach (var item in sccStreamObject)
                    {
                        System.Threading.Thread.Sleep(1);
                        streams.Add(item.Ident);   
                    }
                }          
            }
        }

    }
}
