using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace BackendTestDocplanner.Controllers
{
    /// <summary>
    /// Exposes data to the front application
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly string _basicAuthHeader;

        /// <summary>
        /// Exposes data to the front application
        /// </summary>
        public SlotController(IConfiguration configuration)
        {
            // Get username and password from configuration
            string username = configuration["SlotService:Username"];
            string password = configuration["SlotService:Password"];

            // Generate the Basic authentication header
            _basicAuthHeader = EncodeToBasicAuth(username, password);

            // Output the Basic authentication header for demonstration purposes
            Console.WriteLine(_basicAuthHeader); // This can be removed in production
        }

        /// <summary>
        /// Encodes the username and password in Base64 and adds "Basic" in front.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>Basic authentication string in Base64 format</returns>
        public static string EncodeToBasicAuth(string username, string password)
        {
            // Combine the username and password with a colon
            string credentials = $"{username}:{password}";

            // Convert the credentials string to a byte array using UTF-8 encoding
            byte[] byteCredentials = Encoding.UTF8.GetBytes(credentials);

            // Convert the byte array to a Base64 string
            string base64Credentials = Convert.ToBase64String(byteCredentials);

            // Return the Base64 string with "Basic" prefix
            return $"Basic {base64Credentials}";
        }

        /// <summary>
        /// Devuelve el mensaje "Hello World"
        /// </summary>
        [HttpGet("helloWorld")]
        public IActionResult HelloWorld()
        {
            return Ok(new { Message = _basicAuthHeader });
        }
    }
}
