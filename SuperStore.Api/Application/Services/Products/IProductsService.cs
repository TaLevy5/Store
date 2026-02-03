using Application.DTOs.Products;

namespace Application.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductDto>> GetListAsync(int page,int pageSize,string? sortDir, decimal? minPrice, decimal? maxPrice, string? search);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> PatchAsync(int id, PatchProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}