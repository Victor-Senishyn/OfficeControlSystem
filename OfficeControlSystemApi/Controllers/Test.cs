using Microsoft.AspNetCore.Mvc;

namespace OfficeControlSystemApi.Controllers
{
    public class Test : Controller
    {
        [HttpGet("/test")]
        public IActionResult Test1()
        {
            return Ok("HelloWorld");
        }
    }
}
