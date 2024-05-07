using System.Text;
using System.Text.Json;

namespace DESAFIO_DOTNET_INAUGURAL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZáéíóúâêîôûàèìòùãẽĩõũäëïöüÿÀÉÍÓÚÂÊÎÔÛÀÈÌÒÙÃẼĨÕŨÄËÏÖÜŸ";

            string responseData;

            string apiUrl = "https://fiap-inaugural.azurewebsites.net/fiap";

            while (true)
            {
                var index1 = Random.Shared.Next(0, letras.Length);
                var index2 = Random.Shared.Next(0, letras.Length);

                var numero = Random.Shared.Next(1, 100);

                string chave = letras[index1] + numero.ToString() + letras[index2];

                var json = new
                {
                    Key = chave,
                    grupo = "Room 6"
                };

                string jsonString = JsonSerializer.Serialize(json);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Lendo o conteúdo da resposta
                    responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(chave +" - "+ responseData);

                    if (!string.IsNullOrEmpty(responseData))
                        break;
                }
                else
                {
                    Console.WriteLine("Erro ao acessar a API. Código de status: " + response.StatusCode);
                }
            }

            Console.WriteLine("\n\n\nGrupo " + responseData + " ganhou!");
            Console.ReadLine();
        }
    }
}
