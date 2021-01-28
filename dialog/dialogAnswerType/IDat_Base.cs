using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace AlyaDiscord
{
    interface IDatBase
    {
        // base list for dialogs
        List<DialogData> rootList {get;set;}
        // way how add input to this class
        string input {get;set;}
        // index of list
        int index {get;set;} 
        // vystup
        List<string> outputvar {get;set;}
        // Status message id
        DiscordMessage StatusReportMessageID {get;set;}
        // dialog message id
        DiscordMessage DialogMessageID {get;set;} 
        CommandContext ctx {get;set;}
        Task<dynamic> output();
    }
}
