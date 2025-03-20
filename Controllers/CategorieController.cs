using CrudDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using CrudDotNet.Data;


namespace CrudDotNet.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategorieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Creer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Creer(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(categorie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }

        public IActionResult Modifier(int id)
        {
            var categorie = _context.Categories.Find(id);
            if (categorie == null) return NotFound();
            return View(categorie);
        }

        [HttpPost]
        public IActionResult Modifier(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(categorie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }

        public IActionResult Supprimer(int id)
        {
            var categorie = _context.Categories.Find(id);
            if (categorie == null) return NotFound();
            return View(categorie);
        }

        [HttpPost, ActionName("Supprimer")]
        public IActionResult SupprimerConfirme(int id)
        {
            var categorie = _context.Categories.Find(id);
            if (categorie != null)
            {
                _context.Categories.Remove(categorie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
