using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace api.Services
{
    public class SmsService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string SmsApiUrl = "https://sms.witi.me/sms/send.aspx";
        private const string ApiKey = "d45247bf-d99d-422e-add6-ac59c089ef14";

        public async Task<bool> SendSmsAsync(string numero, string mensagem)
        {
            var body = new
            {
                tipo_envio = "token",
                referencia = "envio de api",
                mensagens = new[]
                {
                    new { numero = numero, mensagem = mensagem }
                }
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{SmsApiUrl}?chave={ApiKey}&async=true";

            try
            {
                var response = await client.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar SMS: {ex.Message}");
                return false;
            }
        }
    }
}
