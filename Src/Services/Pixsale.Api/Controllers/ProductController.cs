using Microsoft.AspNetCore.Mvc;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hello from WeatherForecastController!" });
        }
    }
}
