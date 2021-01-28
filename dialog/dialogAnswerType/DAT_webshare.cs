using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlyaDiscord
{
    public class DatWebshareUrlID : DatBase
    {
        public DatWebshareUrlID(List<DialogData> rootList) : base(rootList){}

        protected override string processingInternalAsync(string input)
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
            // maybe return none
            return input;
        }
    }
}
