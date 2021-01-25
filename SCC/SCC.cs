using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AlyaDiscord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class SCC
{
    private string DiscordToken;
    public SCC(string discordtoken) => DiscordToken = discordtoken;

    public static async Task<string> SearchStreamAsync(string find)
    {
        using (var httpClient = new WebClient())
        {
            WebRequest request = WebRequest.Create("https://plugin.sc2.zone/api/media/filter/search?order=desc&sort=score&type=*&value=" + find + "&access_token=" + "th2tdy0no8v1zoh1fs59");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                return responseString;
            }
        }
    }

    public static async Task<dynamic> GetInfoParentAsync(string parentID)
    {
        using (var httpClient = new WebClient())
        {
            WebRequest request = WebRequest.Create("https://plugin.sc2.zone/api/media/filter/parent?value=" + parentID + "&sort=episode" + "&access_token=" + "th2tdy0no8v1zoh1fs59");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                return responseString;
            }
        }
    }

    public static async Task<dynamic> GetInfoStreamAsync(string parentID)
    {
        using (var httpClient = new WebClient())
        {
            WebRequest request = WebRequest.Create("https://plugin.sc2.zone/api/media/" + parentID + "/streams" + "?access_token=" + "th2tdy0no8v1zoh1fs59");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                return responseString;
            }
        }
    }


}