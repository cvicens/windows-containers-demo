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
        protected string TitleNew { get; private set; }
        protected string Description { get; private set; }
        protected string Details { get; private set; }
        private string _host = "localhost";
        private int _port = 5002;
        private string _uri = "about";

        private readonly HttpClient _httpClient;


        // private readonly ILogger<About> _logger;

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public About()
        // public About(ILogger<About> logger, HttpClient httpClient)
        {
            // _logger = logger;

        //     _httpClient = httpClient;


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
                // _logger.LogInformation("BACKEND_PORT is NULL");
                Console.WriteLine("BACKEND_PORT is NULL");
            }
            catch (Exception e)
            {
                // _logger.LogError("error => " + e.Message);
                Console.WriteLine("error => " + e.Message);
            }

            if (Environment.GetEnvironmentVariable("BACKEND_URI") != null)
            {
                _uri = Environment.GetEnvironmentVariable("BACKEND_URI");
            }

            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri("http://" + _host + ":" + _port + "/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var rng = new Random();

            TitleNew = "Title UNO";
            
            try{
                var aboutType = GetAbout();
                // TitleNew = aboutType.Title;
                // Description = aboutType.Description;
                // Details = aboutType.Details;
            } catch(Exception ex) {
                Description += " OOPS => " + ex.Message;    
            } finally {
                Details += " Finally " + rng.Next(10);
            }
            
        }

        protected AboutType GetAbout()
        {
            var rng = new Random();

            // TitleNew += " Title DOS "  + rng.Next(100);

            

            // TitleNew = "Title CINCO "  + rng.Next(100);

            TitleNew += " BaseAddress " + _httpClient.BaseAddress;

            try
            {
                var responseTask = _httpClient.GetAsync(_uri);
                responseTask.Wait();

                var response = responseTask.Result;
                TitleNew += " response " + response.IsSuccessStatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    // _logger.LogError("Problem when calling client service code: " + response.StatusCode);
                    Console.WriteLine("Problem when calling client service code: " + response.StatusCode);
                    // Description = "ERRPR";
                }

                Description += " CODE: " + response.StatusCode;

                TitleNew += " CODE: " + response.StatusCode;
                response.EnsureSuccessStatusCode();

                var responseStreamTask = response.Content.ReadAsStringAsync();
                responseStreamTask.Wait();
                var responseStream = responseStreamTask.Result;

                Description += " responseStreamTask.Status: " + responseStream;

                var aboutType = JsonSerializer.Deserialize<AboutType>(responseStream);
                
                Description += " aboutType: " + aboutType;
                Details += aboutType.Details + " " +  + rng.Next(30);

                return aboutType;
            }
            catch (Exception e)
            {
                Description += "Exception caught " + e.Message;
                Console.WriteLine("{0} Exception caught.", e);
            }
            finally {
                Description += " In finally";
            }
            
            return null;
        }   
    }
}

public class AboutType
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
}