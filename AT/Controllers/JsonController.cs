using Microsoft.AspNetCore.Mvc;
using AT.WebParsers.CsMarketParser;
using AT.DTOS;
using System.Web.Http.Description;
using AT.WebParsers.LisSkinsParser;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Eventing.Reader;

namespace AT.Controllers
{

    [ApiController]
    [Route("JsonController")]
    public class JsonController : Controller
    {
        //public ICsMarket MyCsMarket;
        //public JsonController(ICsMarket MyCsMarket, ILisSkins MyLisSkins) 
        //{
        //    this.MyCsMarket = MyCsMarket;
        //    this.MyLisSkins = MyLisSkins;
        //}
        //public ILisSkins MyLisSkins;
        
        [HttpGet]
        [ResponseType(typeof(JsonItemsDTO))]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "marketname")] string marketname)
        {
            if(marketname == "CsMarket" && CsMarket.Json != null)
            {
                return Ok(CsMarket.Json);
            }
            if (marketname == "LisSkins" && LisSkins.Json != null)
            {
                return Ok(LisSkins.Json);
            }
            return Ok();
        }
    }
}

