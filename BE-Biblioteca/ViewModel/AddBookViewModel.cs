using System.ComponentModel.DataAnnotations;

namespace BE_Biblioteca.ViewModel
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(50)]
        public required string? Title { get; set; }

        [Required]
        [StringLength(50)]
        public required string? Author { get; set; }

        [Required]
        [StringLength(50)]
        public required string? Genre { get; set; }

        [Required]
        public required bool Available { get; set; }

        [Required]
        public required string? CoverUrl { get; set; }

    }
}
