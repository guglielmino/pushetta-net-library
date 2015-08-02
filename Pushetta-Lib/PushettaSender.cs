using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.gumino.Pushetta.Exceptions;

namespace com.gumino.Pushetta
{
    public class PushettaSender : IPushettaSender
    {
        private HttpClient httpClient;
        private PushettaConfig config;

        public PushettaSender(PushettaConfig cfg)
        {
            httpClient = new HttpClient();
            config = cfg;
        }

        public void SendMessage(string channel, PushMessage message)
        {
            httpClient.BaseAddress = new Uri(Consts.BASE_URL);
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = String.Format("/api/pushes/{0}/", channel);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);

            string payload = JsonConvert.SerializeObject(message,
                                                        Formatting.Indented, 
                                                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            request
                .Headers
                .Add("Authorization", String.Format("Token {0}", config.APIKey));
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage Response = httpClient.SendAsync(request).Result;

            if((int)Response.StatusCode != 200)
            {
                handleErrorResponse((int)Response.StatusCode, Response.ReasonPhrase);
            }
        }


      

        private static IDictionary<int, Type> exceptionsMap =
           new Dictionary<int, Type>()
        {
            { 403, typeof(PushettaInvalidApiKeyException)},
            { 401, typeof(PushettaInvalidApiKeyException)}
        };
            
        private void handleErrorResponse(int statusCode, string reasonPhrase)
        {
            if (exceptionsMap.ContainsKey(statusCode))
            {
                Exception ex = (PushettaException)Activator.CreateInstance(exceptionsMap[statusCode]);
                throw ex;
            }
            else
            {
                throw new PushettaException(reasonPhrase);
            }
        }
    }
}
