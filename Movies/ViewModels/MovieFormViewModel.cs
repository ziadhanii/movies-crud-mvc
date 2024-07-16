namespace Movies.ViewModels
{
    public class MovieFormViewModel
    {
        [Required, StringLength(250)] public string Title { get; set; }

        public int Year { get; set; }
        [Range(1, 10)] public double Rate { get; set; }

        [Required, StringLength(2500)] public string StoryLine { get; set; }

        public IFormFile Poster { get; set; } = default!;
        [Display(Name = "Genre")] public byte GenreId { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}