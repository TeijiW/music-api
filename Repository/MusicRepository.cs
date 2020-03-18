using System.Collections.Generic;
using System.Linq;
using webapi.Context;
using webapi.Models;

namespace musics_api.Repository
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {

        public MusicRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Music> GetMusicByAuthor()
        {
            return Get().OrderBy(music => music.AuthorId).ToList();
        }
    }
}