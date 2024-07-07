using Microsoft.AspNetCore.Mvc;

namespace BackendTestDocplanner.Controllers
{
    /// <summary>
    /// Exposes data to the front application
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        /// <summary>
        /// Exposes data to the front application
        /// </summary>
        public SlotController() { }

        /// <summary>
        /// Devuelve el mensaje "Hello World"
        /// </summary>
        [HttpGet("helloWorld")]
        public IActionResult HelloWorld()
        {
            return Ok(new { Message = "Hello World" });
        }
    }
}
