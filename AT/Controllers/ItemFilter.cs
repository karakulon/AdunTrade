using AT.WebParsers.CsMarketParser;
using AT.WebParsers.LisSkinsParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT.Controllers
{
    [ApiController]
    [Route("ItemFilter")]
    public class ItemFilter : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "marketname")] string marketname, [FromQuery(Name = "itemname")] string itemname)
        {
            if (marketname == null | itemname == null) 
            {
                return BadRequest();
            }
            if(marketname == "CsMarket")
            {
                foreach (var item in CsMarket.Json["items"]) 
                {
                    if (item["Name"] == itemname)
                    {   
                        return Ok(item);
                    }
                }
            }
            if (marketname == "LisSkins")
            {
                foreach (var item in LisSkins.Json["items"])
                {
                    if (item["Name"] == itemname)
                    {
                        return Ok(item);
                    }
                }
            }
            return Ok();
        }
    }
}
