namespace SuperStore.Application.DTOs.ProductsReviews
{
    public class CreateProductReviewDto
    {
        public int ProductId { get; set;}
        public int Rating { get; set;}
        public string Title { get; set;} = string.Empty;
        public string Comment { get; set;} = string.Empty;
    }
}