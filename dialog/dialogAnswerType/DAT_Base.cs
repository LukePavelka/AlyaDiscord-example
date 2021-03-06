using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace AlyaDiscord
{
    abstract public class DatBase : IDatBase
    {
        public List<DialogData> rootList { get; set; }
        public string input { get; set; }
        public int index { get; set; } 
        public List<string> outputvar { get; set; }
        public DiscordMessage StatusReportMessageID { get; set; } 
        public DiscordMessage DialogMessageID { get; set; } 
        public CommandContext ctx { get; set; }

        public DatBase(List<DialogData> rootList)
        {
            this.rootList = rootList;
        }
        protected abstract string processingInternalAsync(string input);

        internal string processing()
        {
            if (input != null)
            {
                return processingInternalAsync(input);
            }
            return null;
        }
        public virtual async Task<dynamic> output()
        {
            await Task.FromResult(0);
            return processing();
        }
    }
}
