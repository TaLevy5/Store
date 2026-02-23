using Microsoft.EntityFrameworkCore;
using SuperStore.Infrastructure.Data;
using SuperStore.Application.DTOs.ProductsReviews;
using SuperStore.Domain.Entities;

namespace SuperStore.Application.Services.ProductReviews
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly AppDbContext _db;

        public ProductReviewService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProductReviewDto?> GetByIdAsync(int id)
        {
            var review = await _db.ProductReviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

            return (review is null) ? null : ToDto(review);
        }

        public async Task<List<ProductReviewDto>> GetByProductAsync(int productId)
        {
            var reviews = await _db.ProductReviews
            .AsNoTracking()
            .Where(r => r.ProductId == productId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

            return reviews.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductReviewDto dto)
        {
            var review = await _db.ProductReviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review is null) return false;

            review.Rating = dto.Rating;
            review.Title = dto.Title;
            review.Comment = dto.Comment;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _db.ProductReviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return false;

            _db.Remove(review);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ProductReviewDto> CreateReviewAsync(CreateProductReviewDto dto)
        {
            var review = new ProductReview
            {
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Title = dto.Title,
                Comment = dto.Comment
            };

            _db.ProductReviews.Add(review);
            await _db.SaveChangesAsync();
            return ToDto(review);
        }

        private ProductReviewDto ToDto(ProductReview review)
        {
            return new ProductReviewDto
            {
                Id = review.Id,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Comment = review.Comment,
                Title = review.Title,
                CreatedAt = review.CreatedAt
            };

        }
    }
}