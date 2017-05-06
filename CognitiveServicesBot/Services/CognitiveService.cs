using CognitiveServicesBot.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveServicesBot.Services
{
    public class CognitiveServices
    {
        #region Signleton

        private static CognitiveServices _instance;

        public static CognitiveServices Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CognitiveServices();
                return _instance;
            }
        }

        #endregion

        public string APIEndpoint
        {
            get
            {
                return ConfigurationManager.AppSettings["AzureCognitiveServicesEndpoint"];
            }
        }

        public string APIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AzureCognitiveServicesKey"];
            }
        }

        public async Task<CognitiveServiceModel> DescribeImage(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKey);
                var response = await client.PostAsJsonAsync($"{APIEndpoint}/analyze?visualFeatures=Description,Tags,Color,Adult,Faces,Categories,ImageType&details=Celebrities&language=en", new
                {
                    Url = imageUrl
                });
                return await response.Content?.ReadAsAsync<CognitiveServiceModel>();
            }

        }
    }

}