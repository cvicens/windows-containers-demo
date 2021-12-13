using System;
using System.Net.Http;
using Newtonsoft.Json;
// // using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

using System.Web.UI;


namespace aspnetapp
{
    public partial class About : Page
    {
        protected string TitleNew { get; private set; }
        protected string Description { get; private set; }
        protected string Details { get; private set; }

        protected string ErrorMessage { get; private set; }
        private string _host = "localhost";
        private int _port = 5002;
        private string _uri = "about";

        private readonly HttpClient _httpClient;

        public About()
        {
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
                Console.WriteLine("BACKEND_PORT is NULL");
            }
            catch (Exception e)
            {
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
            
            try{
                var aboutType = GetAbout();
                TitleNew = aboutType.Title;
                Description = aboutType.Description;
                Details = aboutType.Details;
            } catch(Exception ex) {
                ErrorMessage += " Exception => " + ex.Message;
            }
        }

        protected AboutType GetAbout()
        {
            try
            {
                var responseTask = _httpClient.GetAsync(_uri);
                responseTask.Wait();

                var response = responseTask.Result;
                if (!response.IsSuccessStatusCode)
                {
                    // _logger.LogError("Problem when calling client service code: " + response.StatusCode);
                    Console.WriteLine("Problem when calling client service code: " + response.StatusCode);
                }

                response.EnsureSuccessStatusCode();

                var responseStreamTask = response.Content.ReadAsStringAsync();
                responseStreamTask.Wait();
                var responseStream = responseStreamTask.Result;

                var aboutType = JsonConvert.DeserializeObject<AboutType>(responseStream);
                return aboutType;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
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