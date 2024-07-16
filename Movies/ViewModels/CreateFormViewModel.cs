namespace Movies.ViewModels
{
    public class CreateFormViewModel : MovieFormViewModel
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
         MaxFileSize(FileSettings.MaxFileSizeInBytes)]

        public IFormFile Poster { get; set; } = default!;
    }
}