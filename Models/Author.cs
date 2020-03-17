using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models {
    [Table ("Authors")]
    public class Author {
        public Author () {
            Musics = new Collection<Music> ();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength (120)]
        public string Name { get; set; }

        public int Age { get; set; }

        public ICollection<Music> Musics { get; set; }
    }
}