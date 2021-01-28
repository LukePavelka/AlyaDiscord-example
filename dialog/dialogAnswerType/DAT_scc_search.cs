using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlyaDiscord
{
    public class DATSccSearch : DatBaseAsync
    {
        public int Page = 1;
        public DATSccSearch(List<DialogData> rootList) : base(rootList){}
        protected override async Task<string> processingInternalAsync(string input)
        {   
            var resultfromapi = await SCC.SearchStreamAsync(input);
            var sccParentObject = AlyaBackend.SccParentObject.FromJson(resultfromapi);
            List<DATSccSearchData> SearchData = new List<DATSccSearchData>();
            int abc = 0;
            foreach (var item in sccParentObject.Data)
            {
                abc = abc + 1;
                var newdata = new DATSccSearchData();
                newdata.index = abc;
                newdata.name = item.Source.InfoLabels.Originaltitle;
                newdata.type = item.Source.InfoLabels.Mediatype;
                newdata.year = item.Source.InfoLabels.Year.ToString();
                newdata.sccid = item.Id;
                if(item.Source.Services.Csfd != null){newdata.csfd = item.Source.Services.Csfd;}
                if(item.Source.Services.Imdb != null){newdata.imdb = item.Source.Services.Imdb;}
                if(item.Source.Services.Tmdb != null){newdata.tmdb = item.Source.Services.Tmdb;}
                if(item.Source.Services.Trakt != null){newdata.trakt = item.Source.Services.Trakt;}
                if(item.Source.Services.Tvdb != null){newdata.tvdb = item.Source.Services.Tvdb;}
                SearchData.Add(newdata);
                if (abc > 9)
                {
                   break;
                }
            }
            string DescriptionFunc = String.Format("Vyberte si film nebo serial, napište číslo Indexu\n");
            DescriptionFunc = DescriptionFunc + String.Format($"Index - Název - Typ - Rok\n");
            foreach (var item in SearchData)
            {
                DescriptionFunc += String.Format($"**{item.index}** - **{item.name}** - {item.type} - {item.year}\n");
            }
            var ChoiceAndShow = new DialogData();
            ChoiceAndShow.ChatQuestion = DescriptionFunc;
            ChoiceAndShow.StatusReportDescription = "Vyber si zaznam";
            ChoiceAndShow.AnswerTypeObject = new DATSccSearchnext(rootList);
            ChoiceAndShow.AnswerTypeObject.searchList = SearchData;
            ChoiceAndShow.InternalDescription = "";
            rootList.Insert(index+1,ChoiceAndShow);
            return String.Format($"{sccParentObject.TotalCount} Položek nalezeno");
        }
    }
}
