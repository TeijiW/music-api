using System.Collections.Generic;

namespace musics_api.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public ICollection<MusicDTO> Musics { get; set; }
    }
}