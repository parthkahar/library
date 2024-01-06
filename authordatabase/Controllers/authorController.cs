using emptables.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;

namespace authordatabase.Controllers
{
    public class authorController : ApiController
    {

        [HttpPost]
        [Route("author/addauthor")]
        [JwtAuthorizationFilter]

        public IHttpActionResult createnewauthor(author_table author)
        {
            try
            {
                if(author==null)
                {
                    return BadRequest("there is no data for authoe");

                }
                authorEntities authorEntities = new authorEntities();
                authorEntities.author_table.Add(author);
                authorEntities.SaveChanges();
                return  Created("done", author);

            }

            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }

        [HttpGet]
        [Route("author/getauthordetails")]
        public IHttpActionResult Get()
        {
            try
            {
                authorEntities authorEntities = new authorEntities();

                var data = authorEntities.author_table.ToList();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
        [HttpGet]
        [Route("author/authorid")]

        public IHttpActionResult getid(int author_id)

        {
            try
            {
                authorEntities authorEntities = new authorEntities();
                author_table data = authorEntities.author_table.FirstOrDefault(e => e.author_id == author_id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        [HttpPut]
        [Route("author/editauthor")]
        public IHttpActionResult updateauthordata(int author_id, author_table author)

        {
            try
            {
                authorEntities authorEntities = new authorEntities();

            
               author_table existingauthor=authorEntities.author_table.FirstOrDefault(e=>e.author_id==author_id);

            if(existingauthor!=null)
                {
                    existingauthor.author_name = author.author_name;
                    existingauthor.author_publisher = author.author_publisher;
                    authorEntities.SaveChanges();
                    return Ok(existingauthor);

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
        [Route("author/deleteauthor")]

        public IHttpActionResult deleteauthordata(int author_id)
        {
            {
                try
                {
                    authorEntities authorEntities = new authorEntities();
                    author_table existingauthor = authorEntities.author_table.FirstOrDefault(e => e.author_id == author_id);
                    authorEntities.author_table.Remove(existingauthor);
                    authorEntities.SaveChanges();
                    return Ok(existingauthor);  

                }
                catch(Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }



    }
}
