using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace CalifornianHealthNewPortal.Models
{
    public class BasicModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasicModel(IHttpClientFactory httpClientFactory)
        {
           _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://localhost:32770/")      /*here are the full url : https://localhost:32770/Identity/Account/Login     https://localhost:32770/Identity/Account/Register*/
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },  /*Use correct names here*/
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();


            }
        }

        public async Task LogInUser()
        {

        }
    }
}
