using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_CORE_MVC.Models {
    public class Movie {
        /*primary key*/
        public int Id { get; set; }
        public string? Title { get; set; }

        [Display(Name = "Release Date")] //웹 페이지에 Release Date 라고 표시됨.
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; } // ?는 nullable property

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
