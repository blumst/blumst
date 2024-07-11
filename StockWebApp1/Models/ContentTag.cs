namespace StockWebApp1.Models
{
    public class ContentTag
    {
        public int ContentId { get; set; }
        public Content Content { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
