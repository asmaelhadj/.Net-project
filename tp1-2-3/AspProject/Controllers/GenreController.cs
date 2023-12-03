using AspProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspProject.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationdbContext _db;
        public GenreController(ApplicationdbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var genres = _db.genres.ToList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            _db.genres.Add(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var m = _db.genres.FirstOrDefault(c => c.Id == id);
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre, Guid id)
        {
            var c = _db.genres.Find(id);
            c.Name = genre.Name;
            //_db.genres.Update(genre);
            try
            {   //breakpoints sur SaveChanges pour effectuer deux edit en 
                //Parrallèle --> Exécution du UPDATE 
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException db)
            {
                TempData["message"] = $"Cannot Add : {db.Message}";
                RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
