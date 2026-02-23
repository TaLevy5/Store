using SuperStore.Application.DTOs.ProductsReviews;
using SuperStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Services.ProductReviews;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _service; 

        public ProductReviewController(IProductReviewService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductReviewDto>> GetById(int id)
        {
            var review = await _service.GetByIdAsync(id);
            if (review is null) return NotFound();

            return Ok(review);
        }
        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<IReadOnlyList<ProductReviewDto>>> GetByProduct(int productId)
        {
            var reviews = await _service.GetByProductAsync(productId);
            return Ok(reviews);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductReviewDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}