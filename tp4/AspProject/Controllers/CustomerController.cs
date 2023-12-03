using AspProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationdbContext _db;
        public CustomerController(ApplicationdbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Customer> customers = _db.customers.ToList();
            return View(customers);
        }

        // Action pour afficher le formulaire de création
        public IActionResult Create()
        {
            // Récupère la liste des membres depuis la base de données (assumons que _db est le contexte de base de données)
            var members = _db.memberShipTypes.ToList();

            // Convertit la liste des membres en une liste de SelectListItem pour peupler la ViewBag
            ViewBag.member = members.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });

            // Retourne la vue du formulaire de création avec la liste des membres
            return View();
        }

        // Action pour traiter la soumission du formulaire de création
        [HttpPost]
        public IActionResult Create(Customer c)
        {
            // Vérifie si le modèle n'est pas valide (c'est-à-dire si la validation a échoué)
            if (!ModelState.IsValid)
            {
                // Récupère à nouveau la liste des membres depuis la base de données
                var members = _db.memberShipTypes.ToList();

                // Convertit la liste des membres en une liste de SelectListItem pour peupler la ViewBag
                ViewBag.member = members.Select(m => new SelectListItem()
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
                ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            // Initialise l'identifiant du client (c.Id) avec une nouvelle valeur de type Guid
            c.Id = new Guid();
            // Ajoute le nouveau client à la base de données
            _db.customers.Add(c);
            // Enregistre les modifications dans la base de données
            _db.SaveChanges();
             return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            return View();

        }

    }
}
