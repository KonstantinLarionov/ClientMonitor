using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientMonitor.Controllers
{

    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public List<DataForEdit> Data { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    }
}
