using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using musics_api.Repository;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public MusicsController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("author")]
        public ActionResult<IEnumerable<Music>> GetByAuthor()
        {
            return _uof.MusicRepository.GetMusicByAuthor().ToList();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Music>> Get()
        {
            return _uof.MusicRepository.Get().ToList();
        }

        [HttpGet("{Id:int:min(1)}", Name = "GetMusic")]
        public ActionResult<Music> GetById(int Id)
        {
            var music = _uof.MusicRepository.GetById(music => music.Id == Id);

            if (music == null) { return NotFound(); }
            return music;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Music music)
        {
            _uof.MusicRepository.Add(music);
            _uof.Commit();
            return new CreatedAtRouteResult("GetMusic", new { id = music.Id }, music);
        }

        [HttpPut("{Id:int:min(1)}")]
        public ActionResult Put(int Id, [FromBody] Music music)
        {
            if (Id != music.Id) { return BadRequest(); }
            _uof.MusicRepository.Update(music);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{Id:int:min(1)}")]
        public ActionResult<Music> Delete(int Id)
        {
            var music = _uof.MusicRepository.GetById(music => music.Id == Id);
            if (music == null) { return NotFound(); }
            _uof.MusicRepository.Delete(music);
            _uof.Commit();
            return music;
        }
    }
}