using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission06_FREDERICK.Models; // Make sure this matches your project name

namespace Mission06_FREDERICK.Controllers
{
    public class HomeController : Controller
    {
        private MovieCollectionContext _context;

        // Constructor: This receives the database connection "service"
        public HomeController(MovieCollectionContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        // GET: This opens the "Add Movie" form
        [HttpGet]
        public IActionResult EnterMovie()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View();
        }
        
        // POST: This handles the form submission (Saving the movie)
        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            _context.Movies.Add(response); // Add record to the database
            _context.SaveChanges(); // Commit the changes

            return View("Confirmation", response);
        }

        public IActionResult movieList()
        {
            //Linq
            var movies = _context.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title).ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

           
            var movie = _context.Movies.Single(x => x.MovieId == id);

            
            return View("EnterMovie", movie); 
        }
        [HttpPost]
        public IActionResult Edit(Movie updatedMovie)
        {
            if (ModelState.IsValid)
            {
                // 1. Update the record in the database
                _context.Update(updatedMovie);
                _context.SaveChanges();

                // 2. Send them back to the list so they can see the change
                return RedirectToAction("movieList");
            }
            else // If the user entered bad data (like year 1800), send them back to the form with errors
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View("EnterMovie", updatedMovie);
            }
            
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Find the specific movie Joel wants to delete
            var movie = _context.Movies.Single(x => x.MovieId == id);
    
            return View(movie); // Send that movie to a Delete.cshtml view
        }
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _context.Movies.Remove(movie); // Tell EF to remove the record
            _context.SaveChanges(); // Commit the change to the .sqlite file
    
            return RedirectToAction("movieList"); // Send them back to the list
        }
    }
    
}