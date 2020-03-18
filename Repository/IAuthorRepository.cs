using System.Collections.Generic;
using webapi.Models;

namespace musics_api.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAuthorMusics();
    }
}