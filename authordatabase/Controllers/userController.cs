using emptables.App_Start;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace authordatabase.Controllers
{
    public class userController : ApiController
    {
        [HttpPost]
        [Route("user/adduser")]
        
        public IHttpActionResult createuser(bookuser user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("there is no data");
                }

                authorEntities authorEntities = new authorEntities();

                authorEntities.bookusers.Add(user);

                authorEntities.SaveChanges();

                return Created("done", user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("user/login")]
        public IHttpActionResult Login(bookuser user)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var retrievedUser = authorEntities.bookusers.FirstOrDefault(e => e.user_id == user.user_id);
                if (retrievedUser != null)

                    if
                        (retrievedUser.user_password == user.user_password)
                {
                    var token = GenerateToken(user.user_password);
                    return Ok(new { Token = token });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("user/allusers")]
        public IHttpActionResult get()
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var data = authorEntities.bookusers.ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("user/edituser")]
        public IHttpActionResult updateuser(string user_id, bookuser user)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                bookuser existinguser = authorEntities.bookusers.FirstOrDefault(e => e.user_id == user_id);
                if (existinguser != null)
                {
                    existinguser.user_name = user.user_name;
                    existinguser.user_publisher = user.user_publisher;
                    authorEntities.SaveChanges();
                    return Ok(existinguser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("user/deleteuser")]
        public IHttpActionResult deleteuser(string user_id)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                bookuser existinguser = authorEntities.bookusers.FirstOrDefault(e => e.user_id == user_id);
                if (existinguser != null)
                {
                    authorEntities.bookusers.Remove(existinguser);
                    authorEntities.SaveChanges();
                    return Ok(existinguser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("73C2FD55D4152D973A5B671C6114DCF6535B443BE2748B9197312B2FB94BB827DEE2942B4238C92529C5F28217"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                // Add additional claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: "GT",
                audience: "Dev",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Adjust the expiration time as needed
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
