using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSharpPlus;

namespace AlyaDiscord
{
    public static class CideStoreTimer
    {
        private static List<CideTimerData> timerList = new List<CideTimerData>();
        [JsonInclude]
        internal static List<CideTimerData> TimerList { get => timerList; set => timerList = value; }
    }
    public class CideTimerData
    {
        [JsonIgnore]
        public dynamic timer { get; set; }
        [JsonInclude]
        public ulong Guild { get; set; }
        [JsonInclude]
        public ulong Channel { get; set; }
    }

    public static class CideTimerOnStartup
    {
        internal static async System.Threading.Tasks.Task LoadAsync(DiscordClient client)
        {
            string jsonString = null;
            try
            {
                jsonString = File.ReadAllText("CideData/ActiveChannel.json");
                if (jsonString != null)
                {
                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(10));
                    CideStoreTimer.TimerList = new List<CideTimerData>();
                    var LoadedTimerData = JsonSerializer.Deserialize<List<CideTimerData>>(jsonString);
                    foreach (var item in LoadedTimerData)
                    {
                        CideTimerData data = new CideTimerData();
                        data.timer = await CideFCTimer.runAsync(client, (ulong)item.Guild, (ulong)item.Channel);
                        data.Guild = item.Guild;
                        data.Channel = item.Channel;   
                        CideStoreTimer.TimerList.Add(data);
                        await CideCommands.SaveToJsonAsync(CideStoreTimer.TimerList);
                    }
                    
                }
            }
            catch (System.Exception)
            {}
        }
    }
}