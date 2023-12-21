using AngleSharp;
using AT.WebParsers.CsMarketParser;

namespace AT.WebParsers.LisSkinsParser
{
    //public interface ILisSkins
    //{
    //    public Dictionary<string, List<Dictionary<string, string>>> Json { get; set; }

    //    public Task GetSuperBistroItems();
    //}
    public static class LisSkins
    {
        public static Dictionary<string, List<Dictionary<string, string>>> Json { get; set; }
        public static async Task GetSuperBistroItems()
        {
            Console.WriteLine("plomp");
            var LisSkins_Items = new Dictionary<string, List<Dictionary<string, string>>>
            {
                ["items"] =
                    new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            ["MarketName"] = "LisSkins",
                            ["Name"] = null,
                            ["Price"] = null,
                        }
                    }
            };
            // ;)
            var config_pg = Configuration.Default.WithDefaultLoader();
            var address_pg = "https://lis-skins.ru/market/csgo/?page=1";
            var context_pg = BrowsingContext.New(config_pg);
            var document_pg = await context_pg.OpenAsync(address_pg);

            // lласт паге
            var last_page_parse = document_pg.GetElementsByClassName("page-link").Select(m => m.TextContent);

            // frontendu ne ponyat'
            int page = 0;
            int count_names_inner = 0;
            int count_names_exterior = 0;
            int count_prices = 0;
            while (page <= Int32.Parse(last_page_parse.ToArray()[8]))
            {
                var config = Configuration.Default.WithDefaultLoader();
                var address = $"https://lis-skins.ru/market/csgo/?page={page}";
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);

                // item name 
                var names_inner = document.GetElementsByClassName("name-inner");
                var titles_names_inner = names_inner.Select(m => m.TextContent);

                // item quality
                var names_exterior = document.GetElementsByClassName("name-exterior");
                var titles_names_exterior = names_exterior.Select(m => m.TextContent);

                // prices
                var prices = document.GetElementsByClassName("price");
                var titles_prices = prices.Select(m => m.TextContent);

                // appending to dict all itemos infos epta
                foreach (var title in titles_names_inner)
                {
                    LisSkins_Items["items"][count_names_inner]["Name"] = title;
                    LisSkins_Items["items"].Add(new Dictionary<string, string>{ ["MarketName"] = "LisSkins" } );
                    count_names_inner++;
                }
                foreach (var title in titles_names_exterior)
                {
                    LisSkins_Items["items"][count_names_exterior]["Name"] += $" {title}";
                    count_names_exterior++;
                }
                foreach (var title in titles_prices)
                {
                    string price = title.Replace(".cls-1{fill:#a73006;}", " ").Replace(" ", "").Replace("\n", "");
                    LisSkins_Items["items"][count_prices]["Price"] = price;
                    count_prices++;
                }
                page++;
            }
            Json = LisSkins_Items;
            Console.WriteLine("plomp");
            await Task.Delay(45000);
            LisSkins.GetSuperBistroItems();
        }
    }
}
