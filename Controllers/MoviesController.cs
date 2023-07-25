using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Models;
using System.Diagnostics;

namespace ASP_CORE_MVC.Controllers {
    public class MoviesController : Controller {
        private readonly ASP_CORE_MVCContext _context;

        public MoviesController(ASP_CORE_MVCContext context) {
            /*의존성 주입 Database context*/
            _context = context;
        }


        //https://localhost:7157/Movies/Index?searchString=g
        //https://localhost:7157/Movies/Index/h
        // GET: Movies -> 이것들을 action mathod 라고 지칭하고 있음.
        //[HttpPost] 이거 이렇게 하면 오류남
        public async Task<IActionResult> Index(string MovieGenre, string searchString) {

            //return _context.Movie != null ?   
            //            View(await _context.Movie.ToListAsync()) :
            //            Problem("Entity set 'ASP_CORE_MVCContext.Movie'  is null.");

            if(_context.Movie == null) {
                return Problem("Entity set 'Context.Movie' is null");
            }

            /*get all genre*/
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                        select m;

            if (!String.IsNullOrEmpty(searchString)) {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(MovieGenre)) {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            //return View(await movies.ToListAsync());
            return View(movieGenreVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed) {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Movies/Details/5
        /*IActionResult 동기일 때는 그냥 반환형으로 하는데 비동기일때는 Task*/
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
            if (ModelState.IsValid) {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        /*Edit.cshtml*/
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null) {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*Bind attribute. -> protect against over-posting , ViewModel*/
        [HttpPost]
        [ValidateAntiForgeryToken] /*this attribute previent forgery of a request*/
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
            if (id != movie.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!MovieExists(movie.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Movie == null) {
                return Problem("Entity set 'ASP_CORE_MVCContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null) {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id) {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
