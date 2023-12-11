using Microsoft.AspNetCore.Mvc;
using AT.WebParsers.CsMarketParser;

namespace AT.Controllers
{
    [ApiController]
    [Route("JsonController")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            CsMarket prejson = new CsMarket();
            var json = await prejson.GetItems();
            return Ok(json);
        }
    }
}

