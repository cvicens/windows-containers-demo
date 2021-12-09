using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AboutController : ControllerBase
    {
        private static readonly string[] Titles = new[]
        {
            "About", "This is the about"
        };

        private static readonly string[] Descriptions = new[]
        {
            "Description 1", "Awesome description"
        };

        private static readonly string[] Details = new[]
        {
            "Awesome details", "Lorem ipsum...", "Howdy blah blah"
        };

        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public About Get()
        {
            var rng = new Random();
            return new About
            {
                Title = Titles[rng.Next(Titles.Length)],
                Description = Descriptions[rng.Next(Descriptions.Length)],
                Details = Details[rng.Next(Details.Length)]
            };
        }
    }
}
