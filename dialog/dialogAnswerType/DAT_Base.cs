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
    abstract public class DatBase
    {
        public List<DialogData> rootList; // base list for dialogs
        public string input; // way how add input to this class
        public int index; // index of list
        public dynamic StatusReportMessageID; // Status message id
        public dynamic DialogMessageID; // dialog message id
        public CommandContext ctx;

        public DatBase(List<DialogData> rootList)
        {
            this.rootList = rootList;
        }


        private string processing()
        {
            if (input != null)
            {
                return null;
            }
            return null;
        }

        public dynamic output()
        {
            //processing();
            return processing();
        }


    }
}
