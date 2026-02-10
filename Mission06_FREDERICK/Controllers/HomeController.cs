using Microsoft.AspNetCore.Mvc;
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
    }
}