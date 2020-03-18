using System.Collections.Generic;
using webapi.Models;

namespace musics_api.Repository
{
    public interface IMusicRepository : IRepository<Music>
    {
        IEnumerable<Music> GetMusicByAuthor();
    }
}