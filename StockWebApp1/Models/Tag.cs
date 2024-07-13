namespace StockWebApp1.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; }
    }
}
