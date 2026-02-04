using Application.DTOs.Products;
using Application.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase 
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service) 
        {
            _service = service;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id) 
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortDir = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? search = null)
            {
                if (page <=0) page = 1;
                if (pageSize <= 0) pageSize = 20;

                var result = await _service.GetListAsync(page, pageSize, sortBy, sortDir, minPrice, maxPrice, search);
                return Ok(result);
            }


            [HttpPost]
            public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto) 
            {
                var created = await _service.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = created.Id}, created);
            }

            [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto) 
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (!updated) return NotFound();
                // 204 is succesful update code with no body
                return NoContent();
            }

            [HttpPatch("{id:int}")]
            public async Task<IActionResult> Patch(int id, [FromBody] PatchProductDto dto)
            {
                var patched = await _service.PatchAsync(id, dto);
                if (!patched) return NotFound();

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