using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Web.UI;


namespace aspnetapp
{
    public partial class About : Page
    {
        private string _host = "localhost";
        private int _port = 5002;
        private string _uri = "WeatherForecast";

        private readonly HttpClient _httpClient;


        private readonly ILogger<About> _logger;

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public About(ILogger<About> logger, HttpClient httpClient)
        {
            _logger = logger;

            _httpClient = httpClient;

            if (Environment.GetEnvironmentVariable("BACKEND_HOST") != null)
            {
                _host = Environment.GetEnvironmentVariable("BACKEND_HOST");
            }

            try
            {
                _port = Int32.Parse(Environment.GetEnvironmentVariable("BACKEND_PORT"));
            }
            catch (ArgumentNullException e)
            {
                _logger.LogInformation("BACKEND_PORT is NULL");
            }
            catch (Exception e)
            {
                _logger.LogError("error => " + e.Message);
            }

            if (Environment.GetEnvironmentVariable("BACKEND_URI") != null)
            {
                _uri = Environment.GetEnvironmentVariable("BACKEND_URI");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async Task<AboutType> GetAbout()
        {
            //var client = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://" + _host + ":" + _port + "/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");

            var response = await _httpClient.GetAsync(_uri);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Problem when calling client service code: " + response.StatusCode);
            }

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            var aboutType = await JsonSerializer.DeserializeAsync<AboutType>(responseStream, options);
            return aboutType;
            //return null;
        }   
    }
}

public class AboutType
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
}