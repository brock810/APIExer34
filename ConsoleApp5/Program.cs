using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        string appSettingsPath = "appsettings.json";
        string json = File.ReadAllText(appSettingsPath);
        JObject appSettings = JObject.Parse(json);
        string apiKey = appSettings.GetValue("apiKey").ToString();

        Console.Write("Enter a city: ");
        string city = Console.ReadLine();

        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

        using (var client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string weatherJson = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JObject.Parse(weatherJson);

                string cityName = weatherData.name;
                double temperature = weatherData.main.temp;
                string weatherDescription = weatherData.weather[0].description;

                Console.WriteLine($"Weather in {cityName}:");
                Console.WriteLine($"Temperature: {temperature}°F");
                Console.WriteLine($"Description: {weatherDescription}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        Console.ReadLine();
    }
}
