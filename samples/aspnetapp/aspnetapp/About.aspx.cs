using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspnetapp
{
    public partial class About : Page
    {
        private string _host = "localhost";
        private int _port = 5002;
        private string _uri = "WeatherForecast";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected About GetAbout()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://" + _host + ":" + _port + "/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");

            var response = await client.GetAsync(_uri);

            if (!response.IsSuccessStatusCode) {
                _logger.LogError("Problem when calling client service code: " + response.StatusCode);
            }

            response.EnsureSuccessStatusCode();
            
            using var responseStream = await response.Content.ReadAsStreamAsync(); 
           
            var weatherForecasts = await JsonSerializer.DeserializeAsync<WeatherForecast[]>(responseStream, options);
            return weatherForecasts;
        }
    }
}