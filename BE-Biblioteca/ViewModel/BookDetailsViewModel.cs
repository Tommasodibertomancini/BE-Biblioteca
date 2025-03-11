namespace BE_Biblioteca.ViewModel
{
    public class BookDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public bool Available { get; set; }

        public string? CoverUrl { get; set; }
    }
}
