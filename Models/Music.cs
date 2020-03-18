using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    [Table("Musics")]
    public class Music
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public int Year { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }
    }
}