using RestSharp;

namespace AlyaDiscord
{
    public class CideFCApis
    {
        static public dynamic getAllFleetCarrier()
        {
            var client = new RestClient("http://0.0.0.0:5000/api/FleetCarrierItem");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var cideFcObject = CideFcObject.FromJson(response.Content);
            return cideFcObject;
        }
    }
}



