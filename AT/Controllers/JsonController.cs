using Microsoft.AspNetCore.Mvc;
using AT.WebParsers.CsMarketParser;
using AT.DTOS;
using System.Web.Http.Description;
using AT.WebParsers.LisSkinsParser;
using System.Security.Cryptography.X509Certificates;

namespace AT.Controllers
{

    [ApiController]
    [Route("JsonController")]
    public class JsonController : ControllerBase
    {
        public ICsMarket MyCsMarket;
        public JsonController(ICsMarket MyCsMarket, ILisSkins MyLisSkins) 
        {
            this.MyCsMarket = MyCsMarket;
            this.MyLisSkins = MyLisSkins;
        }
        public ILisSkins MyLisSkins;
        
        [HttpGet]
        [ResponseType(typeof(JsonItemsDTO))]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "marketname")] string marketname)
        {
            if(marketname == "CsMarket")
            {
                var market = this.MyCsMarket;
                await market.GetSuperBistroItems();
                var json = market.Json;
                return Ok(json);
            }
            if (marketname == "LisSkins")
            {
                var market = this.MyLisSkins;
                await market.GetSuperBistroItems();
                var json = market.Json;
                return Ok(json);
            }
            return Ok();
        }
    }
}

