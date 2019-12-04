using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    public class AccountController : Controller
    {

   
        private readonly ApplicationDbContext _db;
      

        public AccountController( ApplicationDbContext db)
        {
            _db = db;
       
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel userdata)
        {

            if (userdata != null)
            {
                var alreadySaved = _db.UserData.Where(Uid => Uid.UserId == userdata.UserId).FirstOrDefault();

             

                if (ModelState.IsValid)
                {


                    var claims = new[]
                  {
                        new Claim(JwtRegisteredClaimNames.Sub, userdata.FirstName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userdata.UserId )
                    };

                    var loginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                    var token = new JwtSecurityToken(
                        issuer: "ifeoluwa",
                        audience: "ifeoluwa",
                        expires: DateTime.UtcNow.AddYears(1),
                        claims: claims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(loginKey, SecurityAlgorithms.HmacSha256)
                        );

                    if (alreadySaved != null)
                    {
                        return Ok(new
                        {
                            id = alreadySaved.Id,
                            message = "User data has already been saved",
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            username = alreadySaved.FirstName,
                            pictureUrl = alreadySaved.PictureUrl,
                            userRole = "user",
                        });
                    }

                    var user = new UserData
                    {
                        UserId = userdata.UserId,
                        FirstName = userdata.FirstName,
                        LastName = userdata.LastName,
                        PictureUrl = userdata.PictureUrl,
                        EmailAddress = userdata.EmailAddress,
                        Provider = userdata.Provider
                    };

                 

                   await _db.AddAsync(user);

                   await _db.SaveChangesAsync();

                    
                  
                    return Ok(new
                    {
                        id = user.Id,
                        message = "User Login successful",
                        username = user.FirstName,
                        pictureUrl = user.PictureUrl,
                        userRole = "user",
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }
            }
           
                var errors = ModelState.Values.First().Errors;

                return BadRequest(new JsonResult(errors));

            

    }
 

    }
         
}
