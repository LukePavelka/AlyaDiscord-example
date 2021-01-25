using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace AlyaDiscord.Cide
{
    public class CideMessages
    {
        public static async Task MakeAndSendMessagesAsync(CideFcObject FC, DiscordMessage msg)
        {
            switch (FC.StatusMode)
            {
                case 1:
                    await RespondMethodOneAsync(FC, msg, RespondMessageBaseBuilder(FC));
                    break;
                case 2:
                    await RespondMethodTwoAsync(FC, msg, RespondMessageBaseBuilder(FC));
                    break;
                case 3:
                    await RespondMethodThreeAsync(FC, msg, RespondMessageBaseBuilder(FC));
                    break;
                case 4:
                    await RespondMethodFourAsync(FC, msg, RespondMessageBaseBuilder(FC));
                    break;
                default:
                    await RespondMethodOneAsync(FC, msg, RespondMessageBaseBuilder(FC));
                    break;
            }

        }

        public static DiscordEmbedBuilder RespondMessageBaseBuilder(CideFcObject FC)
        {
            string CzechTitle = $"Fleet Carrier - {FC.Name}";

            string CzechFCName = "Název";
            string CzechCallSign = "Označení";
            string CzechID = "Identifikátor";

            string CzechDescription = "Base message";
            string CzechCurrentFuel = "Aktuální počet paliva";
            string CzechLocationNow = "Aktuální poloha";

            // if status 2 >> we know + - how many fuel needed for trip
            string CzechNeededFuel = "Palivo potřebné na cestu";

            var BaseRespond = new DiscordEmbedBuilder
            {
                Title = CzechTitle,
                Description = CzechDescription,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "Poslední aktualizace"
                },
                Color = new DiscordColor(25, 118, 210),
                ThumbnailUrl = "https://android.arwwarr.eu/cide_full_white.png"
            };

            // if not null >> add new field and use it!
            if (FC.Name != null)
            {
                BaseRespond.AddField(CzechFCName, FC.Name, true);
            }

            if (FC.Callsign != null)
            {
                BaseRespond.AddField(CzechCallSign, FC.Callsign, true);
            }

            if (FC.Id != null)
            {
                BaseRespond.AddField(CzechID, FC.Id, true);
            }

            if (FC.OwnerCmdr != null)
            {
                BaseRespond.AddField("Majitel",FC.OwnerCmdr);
            }

            if (FC.FuelLevel != null)
            {
                BaseRespond.AddField(CzechCurrentFuel,FC.FuelLevel.ToString(),true);
            }

            if (FC.StatusMode == 2 | FC.StatusMode == 3 && FC.FuelLevel != null && FC.FuelNeeded != null)
            {
                BaseRespond.AddField(CzechNeededFuel,FC.FuelNeeded.ToString(),true);
            }

            if (FC.Location != null)
            {
                BaseRespond.AddField(CzechLocationNow,FC.Location);
            }

            BaseRespond.WithTimestamp(DateTime.Now);
            return BaseRespond;
        }
        public static async Task RespondMethodOneAsync(CideFcObject FC, DiscordMessage msg, DiscordEmbedBuilder BaseEmbed)
        {
            string CzechDescription = "Fáze konstantního monitorování bez určitého cílového systému.";
            string CzechNextJump = "Plánování skoku do systému";
            string CzechNextJumpDateTime = "Skok se uskuteční v";

            BaseEmbed.Description = CzechDescription;
            if (FC.NextJump != null && FC.NextJumpDateTime != null)
            {
                BaseEmbed.AddField(CzechNextJump, FC.NextJump, true);
                BaseEmbed.AddField(CzechNextJumpDateTime, FC.NextJumpDateTime, true);
            }

            await SendToDiscordAsync(msg, BaseEmbed);
        }
        public static async Task RespondMethodTwoAsync(CideFcObject FC, DiscordMessage msg, DiscordEmbedBuilder BaseEmbed)
        {
            BaseEmbed.Description = "Fáze přípravy na dlouhý let, Fleet Carrier zůstane určitý čas v tomto systému pro zadockovaní všech CMDR a nabrání paliva na let.";
            string CzechNextJump = "Destinace plánované cesty";
            string CzechDepartDate = "Datum začátku cesty";
            string CzechDepartTime = "Čas začátku cesty";
            DateTime DateDepart = DateTime.ParseExact(FC.NextJumpDateTime, "HH:mm-dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture);
            string DateDeparting = DateDepart.ToString("dd.MM.yyyy");
            string TimeDeparting = DateDepart.ToString("HH:mm");
            string CzechJumps = "Počet skoků do destinace";
            string CzechdockingAccess = "Docking přístup";
            string CzechAllowNotorious = "Přístup s Notorious";
            string CzechAllowNotoriousAnswer = "";
            BaseEmbed.Color = new DiscordColor(124, 77, 255);

            // bool AllowNotorious to string ANO < > NE
            if(FC.AllowNotorious == true){CzechAllowNotoriousAnswer = "Ano";}else{CzechAllowNotoriousAnswer = "Ne";}

            if (FC.NextJump != null)
            {
                BaseEmbed.AddField(CzechNextJump, FC.NextJump, true);
            }

            if (DateDeparting != null)
            {
                BaseEmbed.AddField(CzechDepartDate, DateDeparting, true);
            }

            if (TimeDeparting != null)
            {
                BaseEmbed.AddField(CzechDepartTime, TimeDeparting, true);
            }

            if (FC.JumpMax != null)
            {
                BaseEmbed.AddField(CzechJumps, FC.JumpMax.ToString(), true);
            }

            if (FC.DockingAccess != null)
            {
                BaseEmbed.AddField(CzechdockingAccess, char.ToUpper(FC.DockingAccess[0]) + FC.DockingAccess.Substring(1), true);
            }

            if (CzechAllowNotoriousAnswer != null)
            {
                BaseEmbed.AddField(CzechAllowNotorious, CzechAllowNotoriousAnswer, true);
            }

            await SendToDiscordAsync(msg, BaseEmbed);
        }
        public static async Task RespondMethodThreeAsync(CideFcObject FC, DiscordMessage msg, DiscordEmbedBuilder BaseEmbed)
        {
            BaseEmbed.Color = new DiscordColor(199, 44, 65);
            BaseEmbed.Description = "Fleet Carrier je na cestě do specifické destinace.";
            string CzechNextJump = "Cíl dalšího skoku";
            string CzechDepartTime = "Čas skoku";
            string TimeDeparting = null;

            if (FC.NextJumpDateTime != null)
            {
                DateTime DateDepart = DateTime.ParseExact(FC.NextJumpDateTime, "HH:mm-dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture);
                TimeDeparting = DateDepart.ToString("HH:mm");
            }
            else
            {
                System.TimeSpan cdjump = new System.TimeSpan(0, 1, 15, 0);
                DateTime localDate = DateTime.Now;
                System.DateTime result = localDate + cdjump;
                TimeDeparting = result.ToString("HH:mm"); 
            }

            string CzechJumpLeft = "Skoků zbývá";
            string CzechJumpMax = "Skoků celkem";
            string CzechFinalDestination = "Finální destinace";
            string CzechTimeDepartTimeFinal = "Čas příletu";
            string CzechTimeDepartDateFinal = "Datum příletu";
            string DepartTimeFinal = null;
            string DepartDateFinal = null;

            if (FC.JumpLeft != null && FC.JumpMax != null)
            {
                BaseEmbed.Color = CalculateColorFromJumps(FC.JumpLeft, FC.JumpMax);
            }

            if (FC.JumpDateTime != null && FC.JumpLeft != null)
            {
                DateTime DateDepartFinal = FinalDateTime(FC.JumpDateTime, FC.JumpLeft);
                DepartTimeFinal = DateDepartFinal.ToString("HH:mm");
                DepartDateFinal = DateDepartFinal.ToString("dd.MM.yyyy");
            }

            if (FC.NextJump != null)
            {
                // next jump
                BaseEmbed.AddField(CzechNextJump, FC.NextJump, true);
            }

            if (TimeDeparting!= null)
            {
                // when will the next jump be time
                BaseEmbed.AddField(CzechDepartTime, TimeDeparting, true);   
            }

            if (FC.JumpLeft != null)
            {
                // how many jump remains
                BaseEmbed.AddField(CzechJumpLeft, FC.JumpLeft.ToString(),true);
            }

            if (FC.JumpMax != null)
            {
                // how many jumps remain maximum
                BaseEmbed.AddField(CzechJumpMax, FC.JumpMax.ToString(), false);
            }

            if (FC.Destination != null)
            {
                // the name of the final destination system
                BaseEmbed.AddField(CzechFinalDestination, FC.Destination, true);
            }

            if (DepartTimeFinal != null)
            {
                // estimated date of arrival at the final destination
                BaseEmbed.AddField(CzechTimeDepartTimeFinal, DepartTimeFinal, true);   
            }

            if (DepartDateFinal != null)
            {
                // estimated time of arrival at the final destination
                BaseEmbed.AddField(CzechTimeDepartDateFinal, DepartDateFinal, true);
            }
            
            await SendToDiscordAsync(msg, BaseEmbed);
        }

        private static async Task RespondMethodFourAsync(CideFcObject FC, DiscordMessage msg, DiscordEmbedBuilder BaseEmbed)
        {
            BaseEmbed.Color = new DiscordColor(53, 255, 105);
            BaseEmbed.Description = "Fleet Carrier dorazil do cíle.";
            await SendToDiscordAsync(msg, BaseEmbed);
        }

        private static DateTime FinalDateTime(string jumpDateTime, long? jumpLeft)
        {
            DateTime Time = DateTime.ParseExact(jumpDateTime, "HH:mm-dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture);
            for (int i = 0; i < jumpLeft; i++)
            {
                System.TimeSpan cdjump = new System.TimeSpan(0, 0, 21, 0);
                Time = Time + cdjump;
            }
            return Time;
        }

        public static async Task SendToDiscordAsync(DiscordMessage msg,DiscordEmbedBuilder emb)
        {
            await msg.ModifyAsync(embed: emb, content: "");
        }

        private static DiscordColor CalculateColorFromJumps(long? jumpLeft, long? jumpMax)
        {
            float r = ((float)jumpMax-(float)jumpLeft)/(float)jumpMax * ((float)180/(float)255);
            float g = ((float)1 - (float)r);
            float b = (float)0;
            return new DiscordColor(g,r,b);
        }
    }
}



