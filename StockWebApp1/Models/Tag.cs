namespace StockWebApp1.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; }
    }
}
