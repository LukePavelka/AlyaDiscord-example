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
    public class DatMultipleChoice
    {
        public List<DialogData> rootList;
        public string input;
        public int index;
        public List<DialogData> Choices;
        public dynamic StatusReportMessageID; // Status message id
        public dynamic DialogMessageID; // dialog message id
        public CommandContext ctx;
        public DatMultipleChoice(List<DialogData> rootList)
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
                    var addStream1  = new DialogData();
                    var addStream2  = new DialogData();
                    var addStream3  = new DialogData();
                    var addStream4  = new DialogData();

                    addStream1.ChatQuestion = "Jaké chcete využit servisní služby? [**csfd** | **tmdb** | **imdb** | **trakt**]\nMužete napsat jen jednu nebo více za sebou, oddělené mezerou\nNapřiklad: **csfd trakt tmdb**";
                    addStream1.StatusReportDescription = "Servisní služby";
                    addStream1.AnswerTypeObject = new DatServices(rootList);
                    addStream1.InternalDescription = "";

                    addStream2.ChatQuestion = "jaké chcete přidat **Webshare streamy**\nmužete napsat **ID** nebo **URL odkazy**\nVedle oddělenou mezerou nebo na nový řadek\nNapřiklad: **wsid1 wsid2 wsid3**";
                    addStream2.StatusReportDescription = "Webshare Streamy";
                    addStream2.AnswerTypeObject = new DatWebshareUrlID(rootList);
                    addStream2.InternalDescription = "Webshare_Streams";

                    addStream3.ChatQuestion = "Jakou Defaultní službu? [**csfd** | **tmdb** | **imdb** | **trakt**]\nMužete si vybrat **jen jednu**.";
                    addStream3.StatusReportDescription = "Default services";
                    addStream3.AnswerTypeObject = null;
                    addStream3.InternalDescription = "default_services_flag";

                    addStream4.ChatQuestion = "Only stream **0** nebo **1**\n Napište číslo **0** Pokud chcete **získat data** pro **kompletně celý kořen**\nNapište číslo **1** Pokud chcete **ziskat Data** **jen pro konkretní věc kterou přidaváte**";
                    addStream4.StatusReportDescription = "Only Stream";
                    addStream4.AnswerTypeObject = null;
                    addStream4.InternalDescription = "only_stream_flag";

                    rootList.Insert(index+1,addStream4);
                    rootList.Insert(index+1,addStream3);
                    rootList.Insert(index+1,addStream2);
                    rootList.Insert(index+1,addStream1);

                    
                    return "Add Streams to Movie";
                }
                if(input == "2")
                {
                    return null;
                }
                if(input == "3")
                {
                    return null;
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
