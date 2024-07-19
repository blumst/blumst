namespace StockWebApp1.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; }
    }
}
