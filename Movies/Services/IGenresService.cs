namespace Movies.Services
{
    public interface IGenresService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}