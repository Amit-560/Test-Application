using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(IConfiguration configuration,ApplicationDbContext context, ILogger<WeatherForecastController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

       
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public object Post([FromForm] InputRequestJson model)
        {
            if (ModelState.IsValid)
            {
                var path = _configuration.GetValue<string>("FolderLocation") + model.File.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
                var jsonData = JsonConvert.DeserializeObject<Data>(model.data);
                InputRequest request = new InputRequest
                {
                    DateOfUpload = DateTime.ParseExact(jsonData.DateOfUpload, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentName = jsonData.DocumentName,
                    DocumentURI = jsonData.DocumentURI,
                };
                _context.Request.Add(request);
                _context.SaveChanges();
                return new { Status = true };
            }
            else
            {
                return new { Status = false, Message = string.Join("|", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage)) };
            }
        }
    }
}
