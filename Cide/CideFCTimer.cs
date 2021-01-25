using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using AlyaDiscord.Cide;

namespace AlyaDiscord
{
    public class CideFCTimer
    {
        static private async Task DeleteAllInChannel(DiscordClient client, ulong channel)
        {
            var channelContext = await client.GetChannelAsync(channel);
            var messageList = await channelContext.GetMessagesAsync();
            if (messageList.Count != 0)
            {
                await channelContext.DeleteMessagesAsync(messageList);
            }
        }

        static private async Task<List<DiscordMessage>> DiscordCreateBlankMessages(DiscordClient client, ulong id)
        {
            var FClist = CideFCApis.getAllFleetCarrier();
            var MessageIDList = new List<DiscordMessage>();
            if (FClist != null)
            {
                foreach (var item in FClist)
                {
                    if (item != null)
                    {
                        DiscordChannel channel = await client.GetChannelAsync(id);
                        MessageIDList.Add(await channel.SendMessageAsync("Alya: Starting Preworking"));
                    }
                }
                return MessageIDList;
            }
            return MessageIDList;
        }

        static public async Task processingAsync(DiscordClient client, ulong channel, ulong guild)
        {
            var guildContext = await client.GetGuildAsync(guild);
            var channelContext = guildContext.GetChannel(channel);
            // ziska list z počtu id ze instanci db
            //var MessageIDList = await DiscordCreateBlankMessages(client, channel);
            IReadOnlyList<DiscordMessage> MessageInChannel = await DiscordCountMessageInChannelAsync(client, channel);

            List<CideFcObject> FClist = CideFCApis.getAllFleetCarrier();
            if (FClist != null)
            {
                if (MessageInChannel.Count == FClist.Count)
                {
                    var FCAndMSG = FClist.Zip(MessageInChannel, (n, w) => new { FC = n, MSG = w });
                    foreach (var item in FCAndMSG)
                    {
                        await CideMessages.MakeAndSendMessagesAsync(item.FC, item.MSG);
                    } 
                    
                    if (guildContext.Name != null)
                    {
                        Console.WriteLine($"CIDE: Update executed in {guildContext.Name} inside channel {channelContext.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"CIDE: Update executed inside channel {channelContext.Name}");
                    }  
                }
                else
                {
                    await DeleteAllInChannel(client, channel);
                    await DiscordCreateBlankMessages(client, channel);
                    await processingAsync(client,channel,guild);
                }
            }
        }

        private static async Task<IReadOnlyList<DiscordMessage>> DiscordCountMessageInChannelAsync(DiscordClient client, ulong channel)
        {
            DiscordChannel DiscordChannel = await client.GetChannelAsync(channel);
            return await DiscordChannel.GetMessagesAsync();

        }

        static public async Task<dynamic> runAsync(DiscordClient client, ulong guild, ulong channel)
        {
            await DeleteAllInChannel(client, channel);
            var timer = new System.Threading.Timer(async e => await processingAsync(client, channel, guild), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return timer;
        }
    }
}



