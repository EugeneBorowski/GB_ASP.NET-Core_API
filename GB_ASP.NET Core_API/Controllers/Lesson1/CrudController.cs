using System;
using Microsoft.AspNetCore.Mvc;

namespace GB_ASP.NET_Core_API.Controllers.Lesson1
{
    [Route("api/")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        //private readonly ILogger<WeatherForecastController> _logger;

        public CrudController(ValuesHolder holder)
        {
            //_logger = logger;ILogger<WeatherForecastController> logger,
            _holder = holder;
        }

        [HttpPost, Route("create/{date}/{temp}")]
        public IActionResult Create([FromRoute] string date, int temp)
        {
            var values = new WeatherForecast() { Date = DateTime.Parse(date), TemperatureC = temp };
            _holder.Values.Add(values);
            var result = "added value Date: " + values.Date + " Temp: " + values.TemperatureC;
            return Ok(result);
        }

        [HttpGet, Route("read/{from}/{to}")]
        public IActionResult Read([FromRoute] string from,string to)
        {
            var result = "Search result:" + "\n";
            foreach (var value in _holder.Values)
            {
                if (DateTime.Parse(from) <= value.Date && DateTime.Parse(to) >= value.Date)
                {
                    result += "Date: " + value.Date + " Temp: " + value.TemperatureC + "\n";
                }
            }
            return Ok(result);
        }

        [HttpPut, Route("update/{date}/{temp}")]
        public IActionResult Update([FromRoute] string date, int temp)
        {
            var count = 0;
            foreach (var value in _holder.Values)
            {
                if (DateTime.Parse(date) == value.Date)
                {
                    value.TemperatureC = temp;
                    count++;
                }
            }
            return Ok("Items edited: " + count);

        }

        [HttpDelete, Route("delete/{from}/{to}")]
        public IActionResult Delete([FromRoute] string from, string to)
        {
            var count = 0;
            foreach (var value in _holder.Values)
            {
                if (DateTime.Parse(from) <= value.Date && DateTime.Parse(to) >= value.Date)
                {
                    count++;
                }
            }
            _holder.Values.RemoveAll(match => match.Date >= DateTime.Parse(from) && match.Date <= DateTime.Parse(to));
            return Ok("Items removed: "+count);
        }
    }
}
