using AgriConnect.Data;
using AgriConnect.Dtos;
using AgriConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get All Products
        [HttpGet]
        public async Task<IActionResult>GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Farmer)
                .ToListAsync();
            return Ok(products);
        }


        // Add products 
        [Authorize(Roles = "farmer")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {

            var product = new Product
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category,
                Status = dto.Status,
                FarmerId = UserId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
