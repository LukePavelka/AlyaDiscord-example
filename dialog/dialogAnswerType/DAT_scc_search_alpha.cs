using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlyaDiscord
{
    public class DATSccSearchnext : DatBaseAsync
    {
        public List<DATSccSearchData> searchList;
        private DATSccSearchData selected;

        public DATSccSearchnext(List<DialogData> rootList) : base(rootList){}

        protected override async Task<string> processingInternalAsync(string input)
        {
            if (input != null)
            {
                foreach (var item in searchList)
                {
                    if (item.index == Convert.ToInt32(input))
                    {
                        selected = item;
                        var ParentInfo = await SCC.GetInfoParentAsync(item.sccid);
                        AlyaBackend.SccParentObject sccParentObject = AlyaBackend.SccParentObject.FromJson(ParentInfo);
                        var SeasonCheck = new List<bool>();
                        var EpisodeCheck = new List<bool>();
                        foreach (var objekt in sccParentObject.Data)
                        {
                            if (objekt.Source.InfoLabels.Mediatype == "season")
                            {
                                SeasonCheck.Add(true);
                            }
                            else
                            {
                                SeasonCheck.Add(false);
                            }
                            if (objekt.Source.InfoLabels.Mediatype == "episode")
                            {
                                EpisodeCheck.Add(true);
                            }
                            else
                            {
                                EpisodeCheck.Add(false);
                            }
                        }
                        if (SeasonCheck.Contains(true))
                        {
                            List<DATSccSearchData> EpisodesInternalProcessing = new List<DATSccSearchData>();
                            foreach (var obsahSezon in sccParentObject.Data)
                            {
                                var SeasonInfo = await SCC.GetInfoParentAsync(obsahSezon.Id);
                                AlyaBackend.SccParentObject SeasonInfoSeri = AlyaBackend.SccParentObject.FromJson(SeasonInfo);
                                foreach (var Episodes in SeasonInfoSeri.Data)
                                {
                                    if (Episodes.Source.InfoLabels.Mediatype == "episode")
                                    {
                                        var newdata = new DATSccSearchData();
                                        if (Episodes.Source.InfoLabels.Episode != null)
                                        {
                                            newdata.IndexEpisode = Convert.ToInt32(Episodes.Source.InfoLabels.Episode);
                                        } 
                                        if (Episodes.Source.InfoLabels.Season != null)
                                        {
                                            newdata.IndexSeason = Convert.ToInt32(Episodes.Source.InfoLabels.Season);
                                        }
                                        newdata.name = Episodes.Source.InfoLabels.Originaltitle;
                                        newdata.type = Episodes.Source.InfoLabels.Mediatype;
                                        newdata.year = Episodes.Source.InfoLabels.Year.ToString();
                                        newdata.sccid = Episodes.Id;
                                        EpisodesInternalProcessing.Add(newdata);
                                    }
                                }
                            }

                            foreach (var jednotlivedily in EpisodesInternalProcessing)
                            {
                                await jednotlivedily.getStreamsAsync();
                            }


                            var fullObj = new SccObjectMovie();
                            var servicedb = new SccObjectServices();
                            List<SccObjectItem> EpisodesObj = new List<SccObjectItem>();

                            servicedb.csfd = selected.csfd;
                            servicedb.imdb = selected.imdb;
                            servicedb.tmdb = selected.tmdb;
                            servicedb.trakt = selected.trakt;
                            servicedb.tvdb = selected.tvdb;

                            fullObj.services = servicedb;

                    
                            foreach (var item5 in EpisodesInternalProcessing)
                            {
                                SccObjectItem OneEpisodesObj = new SccObjectItem();
                                OneEpisodesObj.episode = item5.IndexEpisode;
                                OneEpisodesObj.season = item5.IndexSeason;
                                OneEpisodesObj.streams = item5.streams;
                                EpisodesObj.Add(OneEpisodesObj);
                            }
                            fullObj.items = EpisodesObj;
                    
                            var output = JsonConvert.SerializeObject(fullObj);

                            byte[] byteArray = Encoding.ASCII.GetBytes( output );
                            MemoryStream stream = new MemoryStream( byteArray );
                            await StatusReportMessageID.RespondWithFileAsync(stream,"result_tvshow.json",content:ctx.User.Mention);
                        
                        }
                        if (EpisodeCheck.Contains(true))
                        {
                            List<DATSccSearchData> EpisodesInternalProcessing = new List<DATSccSearchData>();
                             foreach (var Item6 in sccParentObject.Data)
                                {
                                    if (Item6.Source.InfoLabels.Mediatype == "episode")
                                    {
                                        var newdata = new DATSccSearchData();
                                        if (Item6.Source.InfoLabels.Episode != null)
                                        {
                                            newdata.IndexEpisode = Convert.ToInt32(Item6.Source.InfoLabels.Episode);
                                        } 
                                        if (Item6.Source.InfoLabels.Season != null)
                                        {
                                            newdata.IndexSeason = Convert.ToInt32(Item6.Source.InfoLabels.Season);
                                        }
                                        newdata.name = Item6.Source.InfoLabels.Originaltitle;
                                        newdata.type = Item6.Source.InfoLabels.Mediatype;
                                        newdata.year = Item6.Source.InfoLabels.Year.ToString();
                                        newdata.sccid = Item6.Id;
                                        EpisodesInternalProcessing.Add(newdata);
                                    }
                                }
                            foreach (var jednotlivedily in EpisodesInternalProcessing)
                            {
                                await jednotlivedily.getStreamsAsync();
                            }
                            var fullObj = new SccObjectMovie();
                            var servicedb = new SccObjectServices();
                            List<SccObjectItem> EpisodesObj = new List<SccObjectItem>();

                            servicedb.csfd = selected.csfd;
                            servicedb.imdb = selected.imdb;
                            servicedb.tmdb = selected.tmdb;
                            servicedb.trakt = selected.trakt;
                            servicedb.tvdb = selected.tvdb;

                            fullObj.services = servicedb;

                    
                            foreach (var item5 in EpisodesInternalProcessing)
                            {
                                SccObjectItem OneEpisodesObj = new SccObjectItem();
                                OneEpisodesObj.episode = item5.IndexEpisode;
                                OneEpisodesObj.streams = item5.streams;
                                EpisodesObj.Add(OneEpisodesObj);
                            }
                            fullObj.items = EpisodesObj;
                    
                            var output = JsonConvert.SerializeObject(fullObj);

                            byte[] byteArray = Encoding.ASCII.GetBytes( output );
                            MemoryStream stream = new MemoryStream( byteArray );
                            await StatusReportMessageID.RespondWithFileAsync(stream,"result_tvshow_episode_in_root.json",content:ctx.User.Mention);
    
                        }
                        if (selected.type == "movie")
                        {
                            await selected.getStreamsAsync();

                            var fullObj = new SccObjectMovie();
                            var servicedb = new SccObjectServices();
                            List<SccObjectItem> EpisodesObj = new List<SccObjectItem>();

                            servicedb.csfd = selected.csfd;
                            servicedb.imdb = selected.imdb;
                            servicedb.tmdb = selected.tmdb;
                            servicedb.trakt = selected.trakt;
                            servicedb.tvdb = selected.tvdb;

                            fullObj.services = servicedb;

                            SccObjectItem OneEpisodesObj = new SccObjectItem();
                            OneEpisodesObj.streams = selected.streams;
                            EpisodesObj.Add(OneEpisodesObj);
                            fullObj.items = EpisodesObj;
                            var output = JsonConvert.SerializeObject(fullObj);
                            byte[] byteArray = Encoding.ASCII.GetBytes( output );
                            MemoryStream stream = new MemoryStream( byteArray );
                            await StatusReportMessageID.RespondWithFileAsync(stream,"result_movie.json",content:ctx.User.Mention);
                        }           
                    }
                }
               return selected.name;
            }
            return null;
        }

    }
}
