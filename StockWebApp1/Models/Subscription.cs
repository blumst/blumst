namespace StockWebApp1.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }
        public int SubscribedToId { get; set; }
        public User SubscribedTo { get; set; }
    }
}
