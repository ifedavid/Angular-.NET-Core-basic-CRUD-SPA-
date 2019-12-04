using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public int TotalDailySpending { get; set; }
        public CategoryController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }



        [Microsoft.AspNetCore.Mvc.HttpGet("{dateId}")]
        public async Task<ActionResult> GetCategories([FromUri] Guid dateId)
        {
           

            var categories = await dbContext.Categories.Where(d => d.IsDeleted == false && d.DailySpendings.DateId == dateId).ToListAsync();

         

            return Ok( new JsonResult(  categories  ));


        }

        [Microsoft.AspNetCore.Mvc.HttpPost("{dateId}")]
        public async Task<IActionResult> AddCategory([Microsoft.AspNetCore.Mvc.FromBody] CategoryModel[] categoryModel, [FromUri]Guid dateId)

        {
            if (ModelState.IsValid)
            {
                if  (dateId == null)
                {
                    return BadRequest(new { message = "no date supplied" });
                }

                var spending = await dbContext.Spendings.FindAsync(dateId);


                if (categoryModel.Length > 0)
                {
                    foreach (var categories in categoryModel)
                    {
                        var savedCategory = dbContext.Categories.Where(c => c.Name == categories.CategoryName && c.DailySpendings.DateId == dateId && c.IsDeleted == false);

                        if (savedCategory.Count() > 0)
                        {
                            return BadRequest(new
                            {
                                message = "Can not continue operation. The expense name "+ categories.CategoryName+" already exists in the database. Please change the expense name"
                            });
                        }

                        var category = new Category
                        {
                            Name = categories.CategoryName,
                            Amount = categories.CategoryAmount,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            DailySpendings = spending 
                        };

                        await dbContext.AddAsync(category);

                       

                    }

                   

                    await dbContext.SaveChangesAsync();

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


        [Microsoft.AspNetCore.Mvc.HttpPut("[action]")]
        public async Task<IActionResult> Update([Microsoft.AspNetCore.Mvc.FromBody]CategoryModel categoryModel)
        {
            var categoryData = dbContext.Categories.Find(categoryModel.Id);

            if (categoryData == null)
            {
                return BadRequest();
            }
            if (categoryData.Name == null && categoryData.Amount <= 0)
            {
                return BadRequest();
            }

            categoryData.Name = categoryModel.CategoryName;
            categoryData.Amount = categoryModel.CategoryAmount;
            categoryData.UpdatedAt = DateTime.UtcNow;

            dbContext.Entry(categoryData).State = EntityState.Modified;

            
                await dbContext.SaveChangesAsync();

            return Ok(new { message = "Update successful" });
            }


        [Microsoft.AspNetCore.Mvc.HttpPost("[action]")]
        public async Task<IActionResult> Delete([Microsoft.AspNetCore.Mvc.FromBody] CategoryModel categoryModel)
        {
            var categoryData = dbContext.Categories.Find(categoryModel.Id);

            if (categoryData == null)
            {
                return BadRequest();
            }

            categoryData.IsDeleted = true;

            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Delete successful" });
        }


    }
    }


    public class CategoryModel
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryAmount { get; set; }
        
        public DailySpendings spending { get; set; }
    }


  
    

