using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace AlyaDiscord
{
    public class TicketRemoveMainQuestion
    {
        public List<DialogData> rootList;
        public string input;
        public int index;
        public List<DialogData> Choices;
        public dynamic StatusReportMessageID; // Status message id
        public dynamic DialogMessageID; // dialog message id
        public CommandContext ctx;
        public TicketRemoveMainQuestion(List<DialogData> rootList)
        {
            this.rootList = rootList;
            //this.Choices = Choices;
        }


        private string processing()
        {
            if (input != null)
            {
                if(input == "1")
                {
                    var WebshareID  = new DialogData();
                    var Reason  = new DialogData();
                    WebshareID.ChatQuestion = "Jaké **Webshare streamy** chcete smazat?\nMůžete napsat **ID** nebo **URL odkazy**\nvedle sebe oddělené mezerou nebo na nový řadek.\nNapřiklad: **wsid1 wsid2 wsid3**";
                    WebshareID.StatusReportDescription = "Webshare Streamy";
                    WebshareID.AnswerTypeObject = new DatWebshareUrlID(rootList);
                    WebshareID.InternalDescription = "Webshare_Streams";

                    Reason.ChatQuestion = "Napiš důvod smazání";
                    Reason.StatusReportDescription = "Důvod";
                    

                    rootList.Insert(index+1,Reason);
                    rootList.Insert(index+1,WebshareID);
                    return "Mazat Streamy";
                }
                if(input == "2")
                {
                    var Service  = new DialogData();
                    var Reason  = new DialogData();
                    var ServiceID  = new DialogData();
                    Service.ChatQuestion = "Jakou službu? [**csfd** | **tmdb** | **imdb** | **trakt** | **tvdb**]\nMůžete vybrat **jen jednu**!";
                    Service.StatusReportDescription = "Default services";
                    Service.AnswerTypeObject = null;
                    Service.InternalDescription = "default_services_flag";

                    Reason.ChatQuestion = "Napiš důvod smazání";
                    Reason.StatusReportDescription = "Důvod";

                    ServiceID.ChatQuestion = $"Zadej **ID** pro **servisní službu**";
                    ServiceID.StatusReportDescription = $"{Service.Answer} ID";
                    ServiceID.InternalDescription = $"{Service.Answer}_id";
                    ServiceID.AnswerTypeObject = null;

                    rootList.Insert(index+1,Reason);
                    rootList.Insert(index+1,ServiceID);
                    rootList.Insert(index+1,Service);
                    return "Mazat filmy";
                }
                if(input == "3")
                {
                    var Service  = new DialogData();
                    var Reason  = new DialogData();
                    var ServiceID  = new DialogData();
                    Service.ChatQuestion = "Jakou službu? [**csfd** | **tmdb** | **imdb** | **trakt**]\nMůžete vybrat **jen jednu**!";
                    Service.StatusReportDescription = "Default services";
                    Service.AnswerTypeObject = null;
                    Service.InternalDescription = "default_services_flag";

                    Reason.ChatQuestion = "Napiš důvod smazaní";
                    Reason.StatusReportDescription = "Důvod";

                    ServiceID.ChatQuestion = $"Zadej **ID** pro **servisní službu**";
                    ServiceID.StatusReportDescription = $"{Service.Answer} ID";
                    ServiceID.InternalDescription = $"{Service.Answer}_id";
                    ServiceID.AnswerTypeObject = null;

                    rootList.Insert(index+1,Reason);
                    rootList.Insert(index+1,ServiceID);
                    rootList.Insert(index+1,Service);
                    return "Mazat seriály";
                }
                if(input == "4")
                {
                    return null;
                }
                if(input == "5")
                {
                    return null;
                }
                return null;
            }
            return null;
        }

        public async Task<dynamic> output()
        {
            //processing();
            await Task.FromResult(0);
            return processing();
        }


    }
}
