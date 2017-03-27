using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace GoDaddyIntegration
{
    class GoDaddyProcessor
    {
        private readonly AppConfig Config;

        public GoDaddyProcessor(AppConfig godaddyAppConfig)
        {
            Config = godaddyAppConfig;

        }

        public IEnumerable<DomainDto> GetDomains()
        {

            string apiPathProjects = $"/v1/domains";
            var verb = Method.GET;
            var payload = new Dictionary<string, string>{{"limit", "1000"}};
            var response = ConfigureClient(verb, apiPathProjects, payload);
            var content = response.Content; // raw content as string
           
            var arrayResult = JsonConvert.DeserializeObject<IEnumerable<DomainDto>>(content);
            //var jsonObj = JsonConvert.DeserializeObject<Rootobject>(content);


            return arrayResult;


        }

        public IEnumerable<DomainDto> GetActiveDomains()
        {

            string apiPathProjects = $"/v1/domains";
            var verb = Method.GET;
            var payload = new Dictionary<string, string> { { "limit", "1000" }, { "statuses", "ACTIVE" } };
            var response = ConfigureClient(verb, apiPathProjects, payload);
            var content = response.Content; // raw content as string

            var arrayResult = JsonConvert.DeserializeObject<IEnumerable<DomainDto>>(content);
            //var jsonObj = JsonConvert.DeserializeObject<Rootobject>(content);


            return arrayResult;


        }

        private IRestResponse ConfigureClient(Method httpVerb, string resourceMethod, Dictionary<string, string> payload)
        {
            var client = new RestClient(Config.BaseUrl);
         //client.Credentials = new NetworkCredential("username", "password");

            var request = new RestRequest(resourceMethod, httpVerb);
            //request.Credentials

            // request.AddHeader("X-Shopper-Id", null);
            request.AddParameter("Authorization", $"sso-key {Config.api_key}:{Config.api_secret}", ParameterType.HttpHeader);
   //         request.AddHeader("sso-key", $"{Config.api_key}:{Config.api_secret}");
            

            if (payload != null)
            {
                foreach (var kvp in payload)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }

            var response = client.Execute(request);
            return response;

        }


    }
}
