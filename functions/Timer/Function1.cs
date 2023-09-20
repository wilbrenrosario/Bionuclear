using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Timer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var url_base = "https://bionuclearapi.azurewebsites.net";

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url_base + "/api/Correos");
                Console.WriteLine(result.StatusCode);
            }
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
