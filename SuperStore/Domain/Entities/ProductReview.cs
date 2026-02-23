namespace SuperStore.Domain.Entities
{
    public class ProductReview
    {
        public int Id { get; set;}
        public int ProductId { get; set;}
        public int CustomerId { get; set;}
        public int Rating { get; set;}
        public string Title { get; set;} = string.Empty;
        public string Comment { get; set;} = string.Empty;
        public DateTime CreatedAt { get; set;} = DateTime.UtcNow;
        public bool IsVerifiedPurchase  { get; set;}
        public bool IsApproved { get; set;}

    }
}