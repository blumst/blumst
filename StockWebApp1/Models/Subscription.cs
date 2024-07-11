using System.ComponentModel.DataAnnotations;

namespace StockWebApp1.Models
{
    public class Subscription
    {
        
        public int SubscriptionId { get; set; }
        public User SubscriptionInfo { get; set; }
    }
}
