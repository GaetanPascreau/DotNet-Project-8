using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;

namespace CalifornianHealthNewPortal.Controllers
{
    public class LoginController : Controller
    {
            private readonly IHttpClientFactory _httpClientFactory;

            public LoginController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task Login(string username, string password)
            {
                var httpClient = _httpClientFactory.CreateClient("IdentityService");

                var todoItemJson = new StringContent(
                    JsonSerializer.Serialize(username),  // and password ??
                    Encoding.UTF8,
                    Application.Json); // using static System.Net.Mime.MediaTypeNames;

                using var httpResponseMessage =
                    await httpClient.PostAsync("Identity/Account/Login", todoItemJson);  // username and password ??
                /*here are the full url : https://localhost:32770/Identity/Account/Login     https://localhost:32770/Identity/Account/Register*/

                httpResponseMessage.EnsureSuccessStatusCode();
            }
    }
}
