namespace AT.WebParsers.CsMarketParser

{
    //public interface ICsMarket
    //{
    //    public Dictionary<string, List<Dictionary<string, string>>> Json { get; set; }

    //    public Task GetSuperBistroItems();
    //}
    public static class CsMarket
    {
        public static Dictionary<string, List<Dictionary<string, string>>> Json {  get; set; }
        
        public static async Task GetJson()
        {
            Console.WriteLine("ok");
            var Csmarket_Items = new Dictionary<string, List<Dictionary<string, string>>>
            {

                ["items"] =
                new List<Dictionary<string, string>>{
                new Dictionary<string, string>
                {
                    ["MarketName"] = "csmarket",
                    ["Name"] = null,
                    ["Price"] = null,
                    ["BuyOrder"] = null
                }
                }
            };

            HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://market.csgo.com/api/v2/prices/class_instance/RUB.json");
            var csmarket = await response.Content.ReadFromJsonAsync<Market>();
            int count = 0;
            foreach (KeyValuePair<string, Items> itm in csmarket.items)
            {
                Csmarket_Items["items"][count]["Name"] = $"{itm.Value.market_hash_name}";
                Csmarket_Items["items"][count]["Price"] = $"{itm.Value.price}";
                Csmarket_Items["items"][count]["BuyOrder"] = $"{itm.Value.buy_order}";
                count++;
                if (count >= 10)
                {
                    break;
                };
                Csmarket_Items["items"].Add(new Dictionary<string, string> { ["MarketName"] = "CsMarket" });
            }
            Json = Csmarket_Items;
            Console.WriteLine("ok");
            await Task.Delay(50000);
            CsMarket.GetJson();
        }
    }
}
