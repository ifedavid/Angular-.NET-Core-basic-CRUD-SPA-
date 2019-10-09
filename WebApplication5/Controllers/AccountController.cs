using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel userdata)
        {
            List<string> Errors = new List<string>();


            var user = new IdentityUser
            {
                Email = userdata.EmailAddress,
                UserName = userdata.EmailAddress,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, userdata.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "customer");
               
                //send confirmation email
                return Ok(new { username = user.Email, status = 1, message = "Registration successful" });

            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    Errors.Add(error.Description);
                }
            }

            return BadRequest(new JsonResult(Errors));


        }
        





        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel userdata)
        {

            var user = await _userManager.FindByEmailAsync(userdata.EmailAddress);

            var role = await _userManager.GetRolesAsync(user);

            if (user != null && await _userManager.CheckPasswordAsync(user, userdata.Password))
            {
                return Ok(new { username = user.Email, userRole = role, message = "Login Successful" });
            }

           
            return Unauthorized(new { LoginError = "Please check your credentials. Email Address/Password not found. Couldn't validate user" });

        }


   
    }
         
}
