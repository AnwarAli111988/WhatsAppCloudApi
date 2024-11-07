using Newtonsoft.Json;
using System.Text;

namespace WhatsAppCloudApi.Services
{
    public class WhatsAppCloudService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public WhatsAppCloudService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(string toPhoneNumber, string message)
        {
            var phoneNumberId = _config["WhatsAppCloudAPI:PhoneNumberId"];
            var accessToken = _config["WhatsAppCloudAPI:AccessToken"];

            var requestUrl = $"https://graph.facebook.com/v17.0/{phoneNumberId}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = toPhoneNumber,
                type = "text",
                text = new { body = message }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
