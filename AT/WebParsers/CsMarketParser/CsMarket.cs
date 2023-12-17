using System.Text.Json.Nodes;
namespace AT.WebParsers.CsMarketParser
{
    public interface ICsMarket
    {
        public Dictionary<string, List<Dictionary<string, dynamic>>> Json { get; set; }

        public Task<Dictionary<string, List<Dictionary<string, dynamic>>>> GetSuperBistroItems();
    }
    public class CsMarket : ICsMarket
    {
        public Dictionary<string, List<Dictionary<string, dynamic>>> Json { get; set; }
        public async Task<Dictionary<string, List<Dictionary<string, dynamic>>>> GetSuperBistroItems()
        {
            var item = new JsonObject();
            var Csmarket_Items = new Dictionary<string, List<Dictionary<string, dynamic>>>
            {

                ["items"] =
                new List<Dictionary<string, dynamic>>{
                new Dictionary<string, dynamic>
                {
                    ["MarketName"] = "csmarket",
                    ["Name"] = null,
                    ["Price"] = null,
                    ["BuyOrder"] = null
                }
                }
            };

            HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://market.csgo.com/api/v2/prices/class_instance/USD.json");
            var csmarket = await response.Content.ReadFromJsonAsync<Market>();
            int count = 0;
            foreach (KeyValuePair<string, Items> itm in csmarket.items)
            {
                Csmarket_Items["items"][count]["Name"] = $"{itm.Value.market_hash_name}";
                Csmarket_Items["items"][count]["Price"] = itm.Value.price;
                Csmarket_Items["items"][count]["BuyOrder"] = itm.Value.buy_order;
                count++;
                if (count >= 10)
                {
                    break;
                };
                Csmarket_Items["items"].Add(new Dictionary<string, dynamic>());
            }

            Json = Csmarket_Items;
            return Json;
            
        }
    }
}
