using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public CategoryModel[] categoryModelArray { get; set; }
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            foreach (var category in categories)
            {
                return Ok( new
                {
                    id = category.Id, 
                    name = category.Name,
                    amount = category.Amount,
                    timeStamp = category.TimeStamp.ToLongDateString()
                });
            };
            return null;
          

        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

    

        // POST: api/Categories
        [HttpPost]

      
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryModel.CategoryName,
                    Amount = categoryModel.categoryAmount,
                    TimeStamp = DateTime.UtcNow
                };

                await _context.AddAsync(category);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = category.Id,
                    saved = "True",
                    message = "save successful"
                });
            }

            var errors = ModelState.Values.First().Errors;

            return BadRequest(new JsonResult(errors));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }

   
}
