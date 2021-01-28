using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace AlyaDiscord
{
    abstract public class DatBaseAsync : IDatBase
    {
        public List<DialogData> rootList { get; set; }
        public string input { get; set; }
        public int index { get; set; } 
        public List<string> outputvar { get; set; }
        public DiscordMessage StatusReportMessageID { get; set; } 
        public DiscordMessage DialogMessageID { get; set; } 
        public CommandContext ctx { get; set; }

        public DatBaseAsync(List<DialogData> rootList)
        {
            this.rootList = rootList;
        }
        protected abstract Task<string> processingInternalAsync(string input);
        internal async Task<string> processingAsync()
        {
            if (input != null)
            {
                return await processingInternalAsync(input);
            }
            return null;
        }
        public virtual async Task<dynamic> output()
        {
            return await processingAsync();
        }
    }
}
