using AspProject.Models;
using AspProject.Services.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationdbContext _db;
        private readonly IMovieService _movieService;
        public MovieController(ApplicationdbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Movie> movies = _db.movies.ToList();
            return View(movies);
        }
        public IActionResult Create()
        {
            var members = _db.genres.ToList();
            ViewBag.member = members.Select(members => new SelectListItem()
            {
                Text = members.Name,
                Value = members.Id.ToString()
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var members = _db.genres.ToList();
                ViewBag.member = members.Select(members => new SelectListItem()
                {
                    Text = members.Name,
                    Value = members.Id.ToString()
                });
                ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            if (movie.ImageFile != null && movie.ImageFile.Length > 0)
            {
                // Enregistrez le fichier image sur le serveur
                var imagePath = Path.Combine("wwwroot/images", movie.ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    movie.ImageFile.CopyTo(stream);
                }

                // Enregistrez le chemin de l'image dans la base de données
                movie.Image = $"/images/{movie.ImageFile.FileName}";
            }
            // Validez les données du formulaire et ajoutez le nouveau film à la base de données
            _db.movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Edit(Guid id)
        {
            // Récupérez le film à partir de la base de données en utilisant l'ID
            var movie = _db.movies.Find(id);

            if (movie == null)
            {
                return NotFound(); // Ou renvoyez une vue d'erreur personnalisée
            }

            return View(movie);
        }
        public IActionResult Delete(Guid id)
        {
            var movie = _db.movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return Content("unable to find Id");
            var c = _db.movies.SingleOrDefault(c => c.Id == id);
            return View(c);
        }

        //Partie LINQ du TP4

        public IActionResult MoviesByGenre(Guid id)
        {
            var movies = _movieService.GetMoviesByGenre(id);
            return View("MoviesByGenre", movies);
        }


        public IActionResult MoviesOrderedAscending()
        {
            var movies = _movieService.GetAllMoviesOrderedAscending();
            return View("MoviesOrderedAscending", movies);
        }

        public IActionResult MoviesByUserDefinedGenre(string name)
        {
            var movies = _movieService.GetMoviesByUserDefinedGenre(name);
            return View("MoviesByUserDefinedGenre", movies);
        }

        [Route("Movie/released/{year}/{month}")]
        public IActionResult ByRelease(int month, int year)
        {
            return Content("month " + month + " year " + year);
        }

        public IActionResult Lister()
        {
            Movie movie = new Movie { Name = "chosenMovie" };
            List<Customer> customers = new List<Customer>
                                        {new Customer{Name="asma"},
                                        new Customer{Name="asouma"},};

            var viewModel = new ViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
    }
}
