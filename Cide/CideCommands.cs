using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System;

namespace AlyaDiscord
{
    public class CideCommands
    {
        List<CideTimerData> TimerList = new List<CideTimerData>();
        [Command("cide_fc_status_start"), Hidden]
        public async Task cide_fleetcarrier_status_start(CommandContext ctx)
        {
            CideTimerData data = new CideTimerData();
            data.timer = await CideFCTimer.runAsync(ctx.Client, ctx.Guild.Id, ctx.Channel.Id);
            data.Guild = ctx.Guild.Id;
            data.Channel = ctx.Channel.Id;   
            CideStoreTimer.TimerList.Add(data);
            await SaveToJsonAsync(CideStoreTimer.TimerList);
        }

        [Command("cide_fc_status_stop"), Hidden]
        public async Task cide_fleetcarrier_status_stop(CommandContext ctx)
        {
            if (CideStoreTimer.TimerList != null)
            {
                foreach (var item in CideStoreTimer.TimerList)
                {
                    if (item.Channel == ctx.Channel.Id)
                    {
                        await item.timer.DisposeAsync();
                    }     
                }
            }
            await SaveToJsonAsync(CideStoreTimer.TimerList);
        }
        public static async Task SaveToJsonAsync(List<CideTimerData> timerList)
        {
            using FileStream createStream = File.Create("CideData/ActiveChannel.json");
            await JsonSerializer.SerializeAsync(createStream, timerList);
        } 
    }
}
