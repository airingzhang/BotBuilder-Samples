namespace LuisBot.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using Newtonsoft.Json;

    public class QnAService
    {
        string responseString = string.Empty;
        private static readonly string QAMakerApiKey = WebConfigurationManager.AppSettings["QAMakerApiKey"];
        private static readonly string KnowledgebaseId = WebConfigurationManager.AppSettings["QAKnowledgeBaseId"]; // Use knowledge base id created.
        private static readonly string QAUri = $"https://westus.api.cognitive.microsoft.com/qnamaker/v1.0/knowledgebases/{KnowledgebaseId}/generateAnswer";

        public async Task<string> getQAAnswer(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", QAMakerApiKey);
                var values = new Dictionary<string, string>
                {
                    { "question", text }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(QAUri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                //var qnaResponse = JsonConvert.DeserializeObject<QnAMakerResult>(responseString);

               
                return responseString;
            }
        }
    }
}
