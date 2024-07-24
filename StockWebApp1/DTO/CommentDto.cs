namespace StockWebApp1.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }  
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
    }
}
