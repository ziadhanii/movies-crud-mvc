namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IToastNotification _notification;

        public MoviesController(IMoviesService moviesService, IGenresService genresService,
            IToastNotification notification)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _notification = notification;
        }


        public IActionResult Index()
        {
            var movies = _moviesService.GetAll();
            return View(movies);
        }


        [HttpGet]
        public IActionResult Create()
        {
            CreateFormViewModel viewModel = new()
            {
                Genres = _genresService.GetSelectList(),
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _genresService.GetSelectList();

                return View(model);
            }

            await _moviesService.Create(model);
            _notification.AddSuccessToastMessage("Movie Created Successfully");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var movie = _moviesService.GetById(id);

            if (movie is null)
                return NotFound();

            return View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _moviesService.GetById(id);

            if (movie is null)
                return NotFound();

            EditFormViewModel viewModel = new()
            {
                Id = id,
                Title = movie.Title,
                StoryLine = movie.StoryLine,
                GenreId = movie.GenreId,
                Rate = movie.Rate,
                Year = movie.Year,
                Genres = _genresService.GetSelectList(),
                CurrentPoster = movie.Poster
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _genresService.GetSelectList();
                return View(model);
            }

            var game = await _moviesService.Update(model);
            if (game is null)
            {
                return BadRequest();
            }

            _notification.AddSuccessToastMessage("Movie Updated Successfully");

            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _moviesService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}