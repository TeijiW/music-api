using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musics_api.Repository;
using webapi.Context;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public AuthorsController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            return _uof.AuthorRepository.Get().ToList();
        }

        [HttpGet("musics")]
        public ActionResult<IEnumerable<Author>> GetAuthorsMusics()
        {
            return _uof.AuthorRepository.GetAuthorMusics().ToList();
        }

        [HttpGet("{Id:int:min(1)}", Name = "GetAuthor")]
        public ActionResult<Author> GetById(int Id)
        {
            var author = _uof.AuthorRepository.GetById(author => author.Id == Id);

            if (author == null) { return NotFound(); }
            return author;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            _uof.AuthorRepository.Add(author);
            _uof.Commit();
            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{Id:int:min(1)}")]
        public ActionResult Put(int Id, [FromBody] Author author)
        {
            if (Id != author.Id) { return BadRequest(); }
            _uof.AuthorRepository.Update(author);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{Id:int:min(1)}")]
        public ActionResult<Author> Delete(int Id)
        {
            var author = _uof.AuthorRepository.GetById(author => author.Id == Id);
            if (author == null) { return NotFound(); }
            _uof.AuthorRepository.Delete(author);
            _uof.Commit();
            return author;
        }
    }
}