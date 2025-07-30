using AgriConnect.Data;
using AgriConnect.Dtos;
using AgriConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExtensionPostController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public ExtensionPostController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get All Posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.ExtensionPosts.ToListAsync();
            return Ok(posts);
        }

        //Add Post
        [Authorize(Roles = "farmer")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ExtensionPostDto dto)
        {

            var post = new ExtensionPost
            {
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                PostedById = UserId



            };

            _context.ExtensionPosts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }
    }
}

