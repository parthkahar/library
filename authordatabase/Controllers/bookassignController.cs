using emptables.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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



    }










}

