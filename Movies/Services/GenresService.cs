
namespace Movies.Services
{
    public class GenresService : IGenresService
    {
        private readonly MoviesDbContext _context;

        public GenresService(MoviesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Genres
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}