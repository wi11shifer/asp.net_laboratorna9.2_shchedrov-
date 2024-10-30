namespace asplb9._2.Components
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using asplb9._2.Models;

    public class WeatherViewComponent : ViewComponent
    {
        private readonly string apiKey = "";

        public async Task<IViewComponentResult> InvokeAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<dynamic>(response);

                var weatherInfo = new Weather
                {
                    Temperature = data.main.temp,
                    Description = data.weather[0].description,
                    Humidity = data.main.humidity,
                    WindSpeed = data.wind.speed,
                    City = data.name
                };

                return View("WeatherWidget", weatherInfo);
            }
        }
    }


}
