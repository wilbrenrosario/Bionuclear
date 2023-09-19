using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ObservadorTimer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var url_base = "https://bionuclearapi.azurewebsites.net/api/Usuarios/registrar";

            using (var httpClient = new HttpClient())
            {
                var songsModel = new RegistrarDtos()
                {
                    usuario = "0",
                    clave = "Srivalli",
                    nombre_completo = "sds",
                    correo_electronico = "wilbrenrosario@gmail.com"
                };

                HttpContent body = new StringContent(JsonConvert.SerializeObject(songsModel), Encoding.UTF8, "application/json");
                httpClient.PostAsync(url_base, body);

                Console.WriteLine($"Corriendo again ....");
            }
        }

        public class RegistrarDtos
        {
            public string usuario { get; set; } //admin001 * user001
            public string clave { get; set; }
            public string nombre_completo { get; set; }
            public string correo_electronico { get; set; }
        }
    }
}
