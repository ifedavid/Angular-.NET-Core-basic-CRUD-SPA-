using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await dbContext.Categories.ToListAsync();

            foreach (var category in categories)
            {
                return Ok(new
                {
                    id = category.Id,
                    name = category.Name,
                    amount = category.Amount,
                    timeStamp = category.TimeStamp.ToLongDateString()
                });
            };
            return null;


        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel[] categoryModel)

        {
            if (ModelState.IsValid)
            {
                if (categoryModel != null)
                {
                    foreach (var categories in categoryModel)
                    {
                        var category = new Category
                        {
                            Name = categories.CategoryName,
                            Amount = categories.categoryAmount,
                            TimeStamp = DateTime.UtcNow
                        };

                        await dbContext.AddAsync(category);

                        await dbContext.SaveChangesAsync();

                    }

                    return Ok(new
                    {
                        saved = "True",
                        message = "save successful"
                    });
                }
                return BadRequest(new
                {
                    message = "No user input supplied"
                });
            }

                var errors = ModelState.Values.First().Errors;
            
            

            return BadRequest(new JsonResult(errors));
        }
    }


    public class CategoryModel
    {

        public string CategoryName { get; set; }
        public int categoryAmount { get; set; }
    }

  
    
}
