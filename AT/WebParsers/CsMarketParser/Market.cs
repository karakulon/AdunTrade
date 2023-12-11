namespace AT.WebParsers.CsMarketParser
{
    public class Market
    {
        public bool success { get; set; }
        public int? time { get; set; }
        public string? currency { get; set; }
        public Dictionary<string, Items>? items { get; set; }
    }
}
