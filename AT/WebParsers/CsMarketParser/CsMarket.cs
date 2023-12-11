using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace AT.WebParsers.CsMarketParser
{
    public class CsMarket
    {
        public async Task<string> GetItems()
        {
            var item = new JsonObject();
            var Csmarket_Items = new Dictionary<string, List<Dictionary<string, string>>>
            {

                ["items"] =
                new List<Dictionary<string, string>>{
                new Dictionary<string, string>
                {
                    ["MarketName"] = "csmarket",
                    ["Name"] = null,
                    ["Price"] = null,
                    ["Buy_order"] = null
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
                Csmarket_Items["items"][count]["Price"] = $"{itm.Value.price}";
                Csmarket_Items["items"][count]["Buy_order"] = $"{itm.Value.buy_order}";
                count++;
                if (count >= 50)
                {
                    break;
                };
                Csmarket_Items["items"].Add(new Dictionary<string, string>());
            }

            var json = JsonConvert.SerializeObject(Csmarket_Items, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
    }
}
