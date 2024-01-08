using emptables.App_Start;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace authordatabase.Controllers
{
    public class bookassignController : ApiController
    {


        [HttpPost]
        [Route("bookaaign/adddata")]
        [JwtAuthorizationFilter]

        public IHttpActionResult createid(bookassign id)

        {
            try
            {
                if (id == null)

                {
                    return BadRequest("id not providd");


                }

                if (id.book_id == null)
                {
                    return BadRequest("Invalid book Id");

                }

                authorEntities authorEntities = new authorEntities();

                authorEntities.bookassigns.Add(id);

                authorEntities.SaveChanges();

                return Created("done", id);


            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("bookaaign/bookid")]

        public IHttpActionResult createbookid(bookassign bookid)

        {
            try
            {
                if (bookid == null)

                {
                    return BadRequest("id not providd");


                }
              

                authorEntities authorEntities = new authorEntities();

                authorEntities.bookassigns.Add(bookid);

                authorEntities.SaveChanges();

                return Created("done", bookid);


            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("bookaaign/userid ")]

        public IHttpActionResult user(bookassign user_id)

        {
            try
            {
                if (user_id == null)

                {
                    return BadRequest("id not providd");


                }

                authorEntities authorEntities = new authorEntities();

                authorEntities.bookassigns.Add(user_id);

                authorEntities.SaveChanges();

                return Created("done", user_id);


            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("bookaaign/alldata")]

        public IHttpActionResult get()

        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var data = authorEntities.bookassigns.ToList();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }



        }



        [HttpGet]
        [Route("bookassign/databyid")]
        public IHttpActionResult display(int id)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var data = authorEntities.bookassigns.FirstOrDefault(e => e.id == id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
   
        
        
        
   
        
        }

        [HttpGet]
        [Route("bookassign/databyuserid")]
        public IHttpActionResult GetUserBooksById(int user_id)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();

                // Fetch book assignments for the given user_id
                var userlist = authorEntities.bookassigns.Where(e => e.user_id == user_id).ToList();

                List<Book_Table> userDetailsBooks = new List<Book_Table>();

                foreach (var assignment in userlist)
                {
                    // Find book details for each assignment
                    var book = authorEntities.Book_Table.FirstOrDefault(b => b.book_id == assignment.book_id);

                    if (book != null)
                    {
                        userDetailsBooks.Add(book);
                    }
                }

                return Ok(userDetailsBooks);
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











