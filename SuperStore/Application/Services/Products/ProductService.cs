using Microsoft.EntityFrameworkCore;
using SuperStore.Application.DTOs.Products;
using SuperStore.Domain.Entities;
using SuperStore.Infrastructure.Data;

namespace SuperStore.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _db.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

            return (product is null) ? null : ToDto(product);
        }

        public async Task<IReadOnlyList<ProductDto>> GetListAsync(
            int page,
            int pageSize,
            string? sortBy,
            string? sortDir,
            decimal? minPrice,
            decimal? maxPrice,
            string? search)
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 20;

                IQueryable<Product> query = _db.Products.AsNoTracking();

                if(!string.IsNullOrWhiteSpace(search))
                {
                    var term = search.Trim();

                    query = query.Where(p => 
                    p.Name.Contains(term));
                }

                if(minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice);
                }
                if(maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice);
                }

                query = ApplySorting(query, sortBy, sortDir);

                var skip = (page - 1) * pageSize;

                var products = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

                return products.Select(ToDto).ToList();
            }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto. Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return ToDto(product);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> PatchAsync(int id, PatchProductDto dto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return false;

            if(dto.Name != null)
            {
                product.Name = dto.Name;
            }
            if(dto.Description != null)
            {
                product.Description = dto.Description;
            }
            if(dto.Price.HasValue)
            {
                product.Price = dto.Price.Value;
            }
            if(dto.StockQuantity.HasValue)
            {
                product.StockQuantity = dto.StockQuantity.Value;
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        private static IQueryable<Product> ApplySorting(IQueryable<Product> query, string? sortBy, string? sortDir)
        {
            var dir = (sortDir ?? "asc").Trim().ToLowerInvariant();
            var descending = dir == "desc";

            var key = (sortBy ?? "createdAt").Trim().ToLowerInvariant();

            // Add only fields you want to expose for sorting
            return key switch
            {
                "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "stockquantity" => descending ? query.OrderByDescending(p => p.StockQuantity) : query.OrderBy(p => p.StockQuantity),
                "createdat" => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)
            };
        }

        private static ProductDto ToDto(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CreatedAt = p.CreatedAt
            };
        }
        
    }
}