namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesDbContext _context;
        private readonly String _imagesPath;

        public MoviesService(MoviesDbContext context
            , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public IEnumerable<Movie> GetAll()
        {
            var movies = _context.Movies
                .OrderByDescending(m => m.Rate)
                .AsNoTracking()
                .Include(d => d.Genre)
                .ToList();
            return movies;
        }

        public Movie? GetById(int id)
        {
            var movie = _context.Movies
                .Include(d => d.Genre)
                .AsNoTracking()
                .SingleOrDefault(d => d.Id == id);
            return movie;
        }

        public async Task Create(CreateFormViewModel model)
        {
            var posterName = await SavePoster(model.Poster);

            Movie movie = new()
            {
                Title = model.Title,
                StoryLine = model.StoryLine,
                Year = model.Year,
                Rate = model.Rate,
                GenreId = model.GenreId,
                Poster = posterName,
            };
            _context.Add(movie);
            _context.SaveChanges();
        }


        public async Task<Movie?> Update(EditFormViewModel model)
        {
            var movie = _context.Movies
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == model.Id);

            if (movie is null)
                return null;

            var hasNewPoster = model.Poster is not null;
            var oldPoster = movie.Poster;

            movie.Title = model.Title;
            movie.StoryLine = model.StoryLine;
            movie.GenreId = model.GenreId;
            movie.Year = model.Year;
            movie.Rate = model.Rate;
            if (hasNewPoster)
            {
                movie.Poster = await SavePoster(model.Poster!);
            }

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewPoster)
                {
                    var poster = Path.Combine(_imagesPath, oldPoster);
                    File.Delete(poster);
                }

                return movie;
            }
            else
            {
                var poster = Path.Combine(_imagesPath, movie.Poster);
                File.Delete(poster);

                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var movie = _context.Movies.Find(id);
            if (movie is null)
            {
                return isDeleted;
            }

            _context.Movies.Remove(movie);


            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var poster = Path.Combine(_imagesPath, movie.Poster);
                File.Delete(poster);
            }

            return isDeleted;
        }

        private async Task<string> SavePoster(IFormFile poster)
        {
            var posterName = $"{Guid.NewGuid()}{Path.GetExtension(poster.FileName)}";

            var path = Path.Combine(_imagesPath, posterName);

            await using var stream = File.Create(path);
            await poster.CopyToAsync(stream);

            return posterName;
        }
    }
}