namespace DataBundleSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int BundleId { get; set; }
        public Bundle? Bundle { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Paid, Delivered
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}