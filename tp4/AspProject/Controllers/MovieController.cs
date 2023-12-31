﻿using AspProject.Models;
using AspProject.Services.ServiceContracts;
using AspProject.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationdbContext _db;
        private readonly IMovieService _movieService;
        public MovieController(ApplicationdbContext db, IMovieService movieService)
        {
            _db = db;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            return View(_movieService.GetAllMovies());
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

            _movieService.CreateMovie(movie);
            return RedirectToAction("Index");

        }


        public IActionResult Edit(Guid id)
        {
            // Récupérez le film à partir de la base de données en utilisant l'ID
            var movie = _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound(); // Ou renvoyez une vue d'erreur personnalisée
            }

            var members = _db.genres.ToList();
            ViewBag.member = members.Select(members => new SelectListItem()
            {
                Text = members.Name,
                Value = members.Id.ToString()

            });

            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,GenreId,CreatedDate,Image")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing movie from the database

                    if (movie == null)
                    {
                        return NotFound();
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
                    _movieService.Edit(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, re-populate the ViewBag with genres and return to the Edit view
            var members = _db.genres.ToList();
            ViewBag.member = members.Select(members => new SelectListItem()
            {
                Text = members.Name,
                Value = members.Id.ToString()
            });

            return View(movie);
        }

        //
        public IActionResult Delete(Guid id)
        {
            var movie = _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var movie = _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }
            // Delete the image file from the /images folder
            if (!string.IsNullOrEmpty(movie.Image))
            {
                var imagePath = Path.Combine("wwwroot", movie.Image.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _movieService.Delete(id);
            return RedirectToAction("Index"); // Redirect to the list after successful deletion
        }

        public IActionResult Details(Guid id)
        {
            // Récupérez le film à partir de la base de données en utilisant l'ID
            var movie = _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound(); // Ou renvoyez une vue d'erreur personnalisée
            }

            return View(movie);
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
    }
}
