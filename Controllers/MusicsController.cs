using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musics_api.DTOs;
using musics_api.Repository;
using webapi.Models;

namespace webapi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public MusicsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("author")]
        public ActionResult<IEnumerable<MusicDTO>> GetByAuthor()
        {
            var musicsByAuthor = _uof.MusicRepository.GetMusicByAuthor().ToList();
            var musicsByAuthorDTO = _mapper.Map<List<MusicDTO>>(musicsByAuthor);
            return musicsByAuthorDTO;
        }
        [HttpGet]
        public ActionResult<IEnumerable<MusicDTO>> Get()
        {
            var musics = _uof.MusicRepository.Get().ToList();
            var musicsDTO = _mapper.Map<List<MusicDTO>>(musics);
            return musicsDTO;
        }

        [HttpGet("{Id:int:min(1)}", Name = "GetMusic")]
        public ActionResult<MusicDTO> GetById(int Id)
        {
            var music = _uof.MusicRepository.GetById(music => music.Id == Id);
            if (music == null) { return NotFound(); }
            var musicDTO = _mapper.Map<MusicDTO>(music);
            return musicDTO;
        }

        [HttpPost]
        public ActionResult Post([FromBody] MusicDTO musicDTO)
        {
            var music = _mapper.Map<Music>(musicDTO);
            _uof.MusicRepository.Add(music);
            _uof.Commit();
            var musicDTOReturn = _mapper.Map<MusicDTO>(music);
            return new CreatedAtRouteResult("GetMusic", new { id = music.Id }, musicDTOReturn);
        }

        [HttpPut("{Id:int:min(1)}")]
        public ActionResult Put(int Id, [FromBody] MusicDTO musicDTO)
        {
            if (Id != musicDTO.Id) { return BadRequest(); }
            var music = _mapper.Map<Music>(musicDTO);
            _uof.MusicRepository.Update(music);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{Id:int:min(1)}")]
        public ActionResult<MusicDTO> Delete(int Id)
        {
            var music = _uof.MusicRepository.GetById(music => music.Id == Id);
            if (music == null) { return NotFound(); }
            _uof.MusicRepository.Delete(music);
            _uof.Commit();
            var musicDTO = _mapper.Map<MusicDTO>(music);
            return musicDTO;
        }
    }
}