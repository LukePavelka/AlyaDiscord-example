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
    public class DatServices
    {
        public List<DialogData> rootList;
        public string input;
        public int index;
        public List<string> services;
        public dynamic StatusReportMessageID; // Status message id
        public dynamic DialogMessageID; // dialog message id
        public CommandContext ctx;
        public DatServices(List<DialogData> rootList)
        {
            this.rootList = rootList;
        }

        private void processing()
        {
            if (input != null)
            {
                services = input.Split(" ").ToList();
                services.Reverse();
                
                foreach (var item in services)
                {
                    var itemtoadd = new DialogData();

                    itemtoadd.ChatQuestion = $"Zadej **ID** nebo **URL odkaz** pro **{item}**";
                    itemtoadd.StatusReportDescription = $"{item} ID";
                    itemtoadd.InternalDescription = $"{item}_id";
                    itemtoadd.AnswerTypeObject = null;

                    rootList.Insert(index+1,itemtoadd);
                }
            }
        }

        public async Task<dynamic> output()
        {
            await Task.FromResult(0);
            processing();
            return string.Join(",", services);
        }
        

    }
}
