using AT.WebParsers.CsMarketParser;
namespace AT.DTOS

{
    public class JsonItemsDTO
    {
        public List<ItemsDTO> Items { get; set; }
    }
    public class ItemsDTO
    {
        public string MarketName { get; set; }
        public string Name { get; set;}
        public string BuyOrder { get; set; }
        public string Price { get; set; }
    }

}
