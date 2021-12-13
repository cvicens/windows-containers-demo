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
            "About from .NET Core", "This is another about from .NET Core"
        };

        private static readonly string[] Descriptions = new[]
        {
            "This is a sample description from a sample .NET Core.", "Awesome description from a no less awesome .NET Core simple service."
        };

        private static readonly string[] Details = new[]
        {
            "Irrelevant details just to fill in a screen... from .NET CORE", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent consequat faucibus nunc id lobortis. Etiam ut vehicula magna, sed molestie quam. Aliquam vestibulum aliquam nisi, quis faucibus urna. Maecenas libero nulla, iaculis sed pellentesque ac, cursus ut elit. Proin pharetra dui in turpis scelerisque varius. Vestibulum vel tortor et urna fermentum ultricies. Suspendisse lobortis elit est, in pellentesque nunc dapibus sed. Donec at risus eget mauris pellentesque convallis id in justo. Pellentesque pretium metus enim, ut condimentum felis dictum quis.", "The devil is in the detail."
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
