using SuperStore.Application.DTOs.ProductsReviews;

namespace SuperStore.Application.Services.ProductReviews
{
    public interface IProductReviewService
    {
        Task<ProductReviewDto> CreateReviewAsync(CreateProductReviewDto dto);

        Task<ProductReviewDto?> GetByIdAsync(int id);
        Task<List<ProductReviewDto>> GetByProductAsync(int productId);
        Task<bool> UpdateAsync(int id, UpdateProductReviewDto dto);
        Task<bool> DeleteAsync(int id);

    }
}