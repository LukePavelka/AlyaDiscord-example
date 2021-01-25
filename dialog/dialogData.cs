using System;
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
    public class DialogData
    {
        public string ChatQuestion {get;set;}
        public string StatusReportDescription {get;set;}
        public string InternalDescription {get;set;}
        public dynamic AnswerTypeObject {get;set;}
        public dynamic Answer {get;set;}
        public string emojigood = ":white_check_mark:";
        public string emojiidk = ":question:";
        public string emojibad = ":red_square:";

    }
}
