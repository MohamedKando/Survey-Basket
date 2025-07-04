using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;
using System.Text.Json;
namespace Task.Controllers
{
    public class CountryController : Controller
    {
        private static string API_KEY = "key";
        public IActionResult Index()
        {
            var str = makeAPICall();
            var viewModel = JsonSerializer.Deserialize<YourViewModel>(str);
            return View();
        }

        static string makeAPICall()
        {
            var URL = new UriBuilder("https://api.openweathermap.org/data/2.5/weather?q=Cairo&appid=86bd8a884ced5b1329 848274c1e35c1d&units=metric");


      
            
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accept", "application/json");
            return client.DownloadString(URL.ToString());
        }
    }
}
