namespace Movies.ViewModels
{
    public class EditFormViewModel : MovieFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentPoster { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
         MaxFileSize(FileSettings.MaxFileSizeInBytes)]

        public IFormFile? Poster { get; set; } = default!;
    }
}