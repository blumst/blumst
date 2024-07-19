namespace StockWebApp1.Models
{
    public class ContentTag : BaseModel
    {
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
