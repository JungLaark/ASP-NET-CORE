using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_CORE_MVC.Models {
    public class MovieGenreViewModel {
        public List<Movie>? Movies { get; set; } /*List of movies*/
        public SelectList? GenresList { get; set; } /*List of Genres. 장르를 선택할 수 있게 한다.*/
        public string? SelectedGenre { get; set; } /*선택된 장르를 포함한다.*/
        public string? SearchString { get; set; }
    }
}
