using emptables.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace authordatabase.Controllers
{
    public class Book_tableController : ApiController
    {
        [HttpPost]
        [Route("booktable/addbook")]
        [JwtAuthorizationFilter]
        public IHttpActionResult CreateBook(Book_Table book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("invalid book");
                }
                authorEntities authorEntities = new authorEntities();
                authorEntities.Book_Table.Add(book);
                authorEntities.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
        [HttpGet]
        [Route("booktable/alldata")]
        public IHttpActionResult GetBook()
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var data = authorEntities.Book_Table.ToList();
                return Ok(data);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("booktable/databyid")]
        public IHttpActionResult GetBook(int book_id)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                var data = authorEntities.Book_Table.FirstOrDefault(e => e.book_id == book_id);
                return Ok(data);


            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }
        [HttpPut]
        [Route("booktable/editdata")]
        public IHttpActionResult UpdateBook_table(int book_id, Book_Table book)
        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                Book_Table exitingbook = authorEntities.Book_Table.FirstOrDefault(e => e.book_id == book_id);
                if (exitingbook != null)
                {

                    exitingbook.book_name = book.book_name;
                    exitingbook.book_author = book.book_author;
                    authorEntities.SaveChanges();
                    return Ok(exitingbook);
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
        [Route("booktable/delete")]
        

        public IHttpActionResult DeleteBook(int book_id)

        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                Book_Table book = authorEntities.Book_Table.FirstOrDefault(e => e.book_id == book_id);
                authorEntities.Book_Table.Remove(book);
                authorEntities.SaveChanges();
                return Ok(authorEntities);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

       
    }
}
