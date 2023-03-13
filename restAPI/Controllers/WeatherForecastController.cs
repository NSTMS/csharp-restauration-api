using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]

        public IEnumerable<WeatherForecast> Get()
        {
            var result = _service.Get();
            return result;
        }
        /* [Route("currentDay")]*/
        [HttpGet("currentDay/{max}")]

        public IEnumerable<WeatherForecast> Get2([FromQuery] int take, [FromRoute] int max)
        {
            var result = _service.Get();
            return result;
        }
        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {
            /*HttpContext.Response.StatusCode = 401;*/
            /*return StatusCode(401, $"Hellow {name}");*/
            return NotFound($"not found {name}");
        }
        public class Temperature
        {
            public int max { get; set; }

            public int min { get; set; }

        }

        [HttpPost("generate")]
        public ActionResult<string> checkResults([FromQuery] int take, [FromBody]Temperature obj)
        {
            var result = _service.Get(take, obj.max, obj.min);
            if (result)
            {
                return StatusCode(400, "coś nie tak");
            }
            else
            {
                return StatusCode(200, $"wszystko git, take={take} min={obj.min} max={obj.max} ");

            }
        }
    }
}
