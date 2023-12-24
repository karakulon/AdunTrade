namespace AT.WebParsers.CsMarketParser

{
    public static class CsMarket
    {
        public static Dictionary<string, List<Dictionary<string, string>>> Json {  get; set; }
        
        public static async Task GetJson()
        {
            try
            {
                Console.WriteLine("ok");
                var Csmarket_Items = new Dictionary<string, List<Dictionary<string, string>>>
                {
                    ["items"] = new List<Dictionary<string, string>>()
                };
                HttpClient client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync("https://market.csgo.com/api/v2/prices/class_instance/RUB.json");
                var csmarket = await response.Content.ReadFromJsonAsync<Market>();
                Console.WriteLine("cs market ok");
                int count = 0;
                foreach (KeyValuePair<string, Items> itm in csmarket.items)
                {
                    Console.WriteLine(itm.Value);
                    Csmarket_Items["items"].Add(new Dictionary<string, string> { ["MarketName"] = "CsMarket" });
                    Csmarket_Items["items"][count]["Name"] = $"{itm.Value.market_hash_name}";
                    Csmarket_Items["items"][count]["Price"] = $"{itm.Value.price}";
                    Csmarket_Items["items"][count]["BuyOrder"] = $"{itm.Value.buy_order}";
                    count++;
                    if (count >= 10)
                    {
                        break;
                    };
                }
                Json = Csmarket_Items;
                Console.WriteLine("ok");
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                await Task.Delay(20000000);
                CsMarket.GetJson();
            }
             
            
        }
    }
}
