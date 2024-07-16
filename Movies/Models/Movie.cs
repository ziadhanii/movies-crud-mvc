namespace Movies.Models;

public class Movie
{
    public int Id { get; set; }

    [Required, MaxLength(255)] public string Title { get; set; }

    public int Year { get; set; }

    public double Rate { get; set; }

    [Required, MaxLength(2500)] public string StoryLine { get; set; }

    [Required, MaxLength(500)] public string Poster { get; set; } = string.Empty;

    public byte GenreId { get; set; }

    public Genre Genre { get; set; }
}