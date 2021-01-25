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

namespace AlyaDiscord
{
    public class CideFCApis
    {
        static public dynamic getAllFleetCarrier()
        {
            var client = new RestClient("http://161.97.71.35:5000/api/FleetCarrierItem");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var cideFcObject = CideFcObject.FromJson(response.Content);
            return cideFcObject;
        }
    }
}



