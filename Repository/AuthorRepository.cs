using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Models;

namespace musics_api.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {

        public AuthorRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Author> GetAuthorMusics()
        {
            return Get().Include(author => author.Musics);
        }
    }
}