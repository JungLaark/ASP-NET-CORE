using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_MVC.Models {
    public class Movie {
        /*primary key*/
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; } // ?는 nullable property
        public decimal Price { get; set; }
    }
}
