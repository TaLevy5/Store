namespace SuperStore.Application.DTOs.ProductsReviews
{
    public class ProductReviewDto
    {
        public int Id { get; set;}
        public int ProductId { get; set;}
        public int Rating { get; set;}
        public string Comment { get; set;} = string.Empty;
        public string Title { get; set;} = string.Empty;
        public DateTime CreatedAt { get; set;} = DateTime.UtcNow;
        
    }
}