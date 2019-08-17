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
        public async Task<IActionResult> RegisterCustomer([FromBody] RegistrationViewModel userdata)
        {
            List<string> Errors = new List<string>();


            var user = new IdentityUser
            {
                Email = userdata.email,
                UserName = userdata.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, userdata.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "customer");

                //send confirmation email
                return Ok(new { username = user.UserName, email = user.Email, status = 1, message = "Registration successful" });

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
        public async Task<IActionResult> RegisterPlanner([FromBody] TourPlannersModel plannerdata)
        {
            List<string> Errors = new List<string>();

            var newPlanner = new TourPlannersModel
            {
                TourPlannerFullname = plannerdata.TourPlannerFullname,
                TourPlannerEmail = plannerdata.TourPlannerEmail,
                TourPlannerPhoneNumber = plannerdata.TourPlannerPhoneNumber,
                AboutTourPlanner = plannerdata.AboutTourPlanner,
                TourPlannerWebsite = plannerdata.TourPlannerWebsite,
                TourPlannerPassword = plannerdata.TourPlannerPassword
            };

            await _db.TourPlanners.AddAsync(newPlanner);
            await _db.SaveChangesAsync();
            var planner = new IdentityUser
            {
                Email = newPlanner.TourPlannerEmail,
                UserName = newPlanner.TourPlannerFullname,
                PhoneNumber = newPlanner.TourPlannerPhoneNumber
              
            };


            var result = await _userManager.CreateAsync(planner, newPlanner.TourPlannerPassword);

            if (result.Succeeded)
            {
               
               

                await _userManager.AddToRoleAsync(planner, "planner");
            

                //send confirmation email
                return Ok(new { username = planner.UserName, email = planner.Email, status = 1, message = "Planner Registration successful" });

                

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






        [HttpPost("[action]/{userType}")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginViewModel userdata)
        {

            var user = await _userManager.FindByNameAsync(userdata.Username);

            var roles = await _userManager.GetRolesAsync(user);

            if (user != null && await _userManager.CheckPasswordAsync(user, userdata.Password))
            {
                return Ok(new { username = user.UserName, userRole = roles, message = "Login Successful" });
            }

            ModelState.AddModelError("", "Username/Password not found");
            return Unauthorized(new { LoginError = "Please check your credentials. Couldn't validate user" });

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> LoginPlanner([FromBody] TourPlannersModel plannerdata)
        {

            var user = await _userManager.FindByNameAsync(plannerdata.TourPlannerFullname);

            var roles = await _userManager.GetRolesAsync(user);

            if (user != null && await _userManager.CheckPasswordAsync(user, plannerdata.TourPlannerPassword))
            {
                return Ok(new { username = user.UserName, userRole = roles, message = "Planner Login Successful" });
            }

            ModelState.AddModelError("", "Username/Password not found");
            return Unauthorized(new { LoginError = "Please check your credentials. Couldn't validate user" });
        }
    }
         
}
