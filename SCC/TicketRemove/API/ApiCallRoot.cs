using RestSharp;
using System.Net;

namespace AlyaDiscord.TicketRemoveAPICall
{
    public class requestRoot
    {
        static public dynamic post(string json)
        {
            var client = new RestClient("http://0.0.0.0:5005/api/SCCRemoveRoot");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return "error";
            }
        }
        static public dynamic put(string json, string id)
        {
            var client = new RestClient($"http://0.0.0.0:5005/api/SCCRemoveRoot/{id}");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK | response.StatusCode == HttpStatusCode.NoContent)
            {
                return response.Content;
            }
            else
            {
                return "error";
            }
        }
        static public dynamic delete(string id)
        {
            var client = new RestClient($"http://0.0.0.0:5005/api/SCCRemoveRoot/{id}");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK | response.StatusCode == HttpStatusCode.NoContent)
            {
                return response.Content;
            }
            else
            {
                return "error";
            }
        }
        static public dynamic get(string id)
        {
            var client = new RestClient($"http://0.0.0.0:5005/api/SCCRemoveRoot/{id}");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return "error";
            }
        }
        static public dynamic getall()
        {
            var client = new RestClient($"http://0.0.0.0:5005/api/SCCRemoveRoot/");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return "error";
            }
        }
    }
}