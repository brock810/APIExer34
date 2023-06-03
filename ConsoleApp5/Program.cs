using System;
using System.Net.Http;
using Newtonsoft.Json;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        string apiKey = "80fada63f67ededd921d41d8604b8685";

        Console.Write("Enter a city: ");
        string city = Console.ReadLine();

        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

        using (var client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JsonConvert.DeserializeObject(json);

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
