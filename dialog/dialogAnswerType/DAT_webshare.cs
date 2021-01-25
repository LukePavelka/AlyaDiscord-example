using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace AlyaDiscord
{
    public class DatWebshareUrlID
    {
        public List<DialogData> rootList;
        public string input;
        public int index;
        public List<string> outputvar;
        public dynamic StatusReportMessageID;
        public dynamic DialogMessageID;
        public CommandContext ctx;
        public DatWebshareUrlID(List<DialogData> rootList)
        {
            this.rootList = rootList;
        }

        private void processing()
        {
            if (input != null)
            {
                string[] lines = input.Split(
                    new[] { "\r\n", "\r", "\n", " " },
                    StringSplitOptions.None
                );
                lines.ToList();
                outputvar = new List<string>();
                Regex r = new Regex(@"(?<=file/)[a-zA-Z0-9]+", RegexOptions.Compiled);

                foreach (var item in lines)
                {
                    if (item.Contains("/"))
                    {
                        Match match = r.Match(item);
                        outputvar.Add(match.Value);
                    }
                    else
                    {
                        outputvar.Add(item);
                    }
                    
                }

                
            }
        }

        public async Task<dynamic> output()
        {
            await Task.FromResult(0);
            processing();
            return "";
        }

        

    }
}
