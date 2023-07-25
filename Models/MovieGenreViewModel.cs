using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_CORE_MVC.Models {
    /*Add Search by genre*/
    public class MovieGenreViewModel {
        public List<Movie>? Movies { get; set; } /*List of movies*/
        public SelectList? Genres { get; set; } /*List of Genres. 장르를 선택할 수 있게 한다.*/
        public string? MovieGenre { get; set; } /*선택된 장르를 포함한다.*/
        public string? SearchString { get; set; } /**/
    }
}
