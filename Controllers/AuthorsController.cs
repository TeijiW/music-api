using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musics_api.DTOs;
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
        private readonly IMapper _mapper;
        public AuthorsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> Get()
        {
            var authors = _uof.AuthorRepository.Get().ToList();
            var authorsDTO = _mapper.Map<List<AuthorDTO>>(authors);
            return authorsDTO;
        }

        [HttpGet("musics")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthorsMusics()
        {
            var authorsMusics = _uof.AuthorRepository.GetAuthorMusics().ToList();
            var authorsMusicsDTO = _mapper.Map<List<AuthorDTO>>(authorsMusics);
            return authorsMusicsDTO;
        }

        [HttpGet("{Id:int:min(1)}", Name = "GetAuthor")]
        public ActionResult<AuthorDTO> GetById(int Id)
        {
            var author = _uof.AuthorRepository.GetById(author => author.Id == Id);

            if (author == null) { return NotFound(); }
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }

        [HttpPost]
        public ActionResult Post([FromBody] AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            _uof.AuthorRepository.Add(author);
            _uof.Commit();
            var authorDTOReturn = _mapper.Map<AuthorDTO>(author);
            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, authorDTOReturn);
        }

        [HttpPut("{Id:int:min(1)}")]
        public ActionResult Put(int Id, [FromBody] AuthorDTO authorDTO)
        {
            if (Id != authorDTO.Id) { return BadRequest(); }
            var author = _mapper.Map<Author>(authorDTO);
            _uof.AuthorRepository.Update(author);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{Id:int:min(1)}")]
        public ActionResult<AuthorDTO> Delete(int Id)
        {
            var author = _uof.AuthorRepository.GetById(author => author.Id == Id);
            if (author == null) { return NotFound(); }
            _uof.AuthorRepository.Delete(author);
            _uof.Commit();
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }
    }
}