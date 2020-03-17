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
    public class MusicsController : ControllerBase {
        private readonly DatabaseContext _context;
        public MusicsController (DatabaseContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetAsync () {
            return await _context.Musics.AsNoTracking ().ToListAsync ();
            // return await _context.Musics
            //     .Include (music => music.Author)
            //     .ThenInclude(author => author.Name)
            //     .ToListAsync ();
        }

        [HttpGet ("{Id:int:min(1)}", Name = "GetMusic")]
        public async Task<ActionResult<Music>> GetByIdAsync (int Id) {
            var music = await _context.Musics
                .AsNoTracking ()
                .FirstOrDefaultAsync (music => music.Id == Id);

            if (music == null) { return NotFound (); }
            return music;
        }

        [HttpPost]
        public ActionResult Post ([FromBody] Music music) {
            _context.Musics.Add (music);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("GetMusic", new { id = music.Id }, music);
        }

        [HttpPut ("{Id:int:min(1)}")]
        public ActionResult Put (int Id, [FromBody] Music music) {
            if (Id != music.Id) { return BadRequest (); }
            _context.Entry (music).State = EntityState.Modified;
            _context.SaveChanges ();
            return Ok ();
        }

        [HttpDelete ("{Id:int:min(1)}")]
        public ActionResult<Music> Delete (int Id) {
            var music = _context.Musics.FirstOrDefault (music => music.Id == Id);
            if (music == null) { return NotFound (); }
            _context.Musics.Remove (music);
            _context.SaveChanges ();
            return music;
        }
    }
}