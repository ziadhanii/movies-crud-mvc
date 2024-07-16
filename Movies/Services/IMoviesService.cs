namespace Movies.Services
{
    public interface IMoviesService
    {
        IEnumerable<Movie> GetAll();
        Movie? GetById(int id);

        Task Create(CreateFormViewModel game);
        Task<Movie?> Update(EditFormViewModel model);

        bool Delete(int id);
    }
}