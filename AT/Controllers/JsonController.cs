using Microsoft.AspNetCore.Mvc;
using AT.WebParsers.CsMarketParser;
using AT.DTOS;
using System.Web.Http.Description;
using AT.WebParsers.LisSkinsParser;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Eventing.Reader;
using System.Collections.Generic;

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
        public async Task<IActionResult> GetAsync([FromQuery(Name = "marketname")] string marketname, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                return BadRequest("Invalid page number. Page number must be greater than or equal to 1.");
            }

            if (pageSize < 1)
            {
                return BadRequest("Invalid page size. Page size must be greater than or equal to 1.");
            }

             List < Dictionary<string, string> >items = null;

            if (marketname == "CsMarket" && CsMarket.Json != null)
            {
                items = CsMarket.Json["items"];
            }
            else if (marketname == "LisSkins" && LisSkins.Json != null)
            {
                items = LisSkins.Json["items"];
            }

            if (items == null)
            {
                return NotFound("Market not found or Json is null.");
            }

            var totalCount = items.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Validate that the requested page number is within the valid range
            if (pageNumber > totalPages)
            {
                return BadRequest($"Invalid page number. The maximum page number is {totalPages}.");
            }

            // Calculate the range of items to return based on the requested page and size
            var startIndex = (pageNumber - 1) * pageSize;
            var pagedItems = items.Skip(startIndex).Take(pageSize);



            return Ok(pagedItems);
        }

    }
}

