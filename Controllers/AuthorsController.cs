using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Models;

namespace webapi.Controllers {
    [Route ("api/[Controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase {
        private readonly DatabaseContext _context;
        public AuthorsController (DatabaseContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAsync () {
            return await _context.Authors.AsNoTracking ().ToListAsync ();
        }

        [HttpGet ("musics")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsMusicsAsync () {
            return await _context.Authors.Include (author => author.Musics).ToListAsync ();
        }

        [HttpGet ("{Id:int:min(1)}", Name = "GetAuthor")]
        public async Task<ActionResult<Author>> GetByIdAsync (int Id) {
            var author = await _context.Authors
                .AsNoTracking ()
                .FirstOrDefaultAsync (author => author.Id == Id);

            if (author == null) { return NotFound (); }
            return author;
        }

        [HttpPost]
        public ActionResult Post ([FromBody] Author author) {
            _context.Authors.Add (author);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut ("{Id:int:min(1)}")]
        public ActionResult Put (int Id, [FromBody] Author author) {
            if (Id != author.Id) { return BadRequest (); }
            _context.Entry (author).State = EntityState.Modified;
            _context.SaveChanges ();
            return Ok ();
        }

        [HttpDelete ("{Id:int:min(1)}")]
        public ActionResult<Author> Delete (int Id) {
            var author = _context.Authors.FirstOrDefault (author => author.Id == Id);
            if (author == null) { return NotFound (); }
            _context.Authors.Remove (author);
            _context.SaveChanges ();
            return author;
        }
    }
}