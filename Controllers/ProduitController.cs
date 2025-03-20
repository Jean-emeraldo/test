using CrudDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudDotNet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CrudDotNet.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produits = _context.Produits.Include(p => p.Categorie).ToList();
            return View(produits);
        }

        public IActionResult Creer()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "id_categorie", "nom_categorie");
            return View();
        }


        [HttpPost]
        public IActionResult Creer(Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Produits.Add(produit);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(produit);
        }

        public IActionResult Modifier(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "id_categorie", "nom_categorie", produit.id_cat);
            return View(produit);
        }


        [HttpPost]
        public IActionResult Modifier(Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Produits.Update(produit);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(produit);
        }

        public IActionResult Supprimer(int id)
        {
            var produit = _context.Produits.Include(p => p.Categorie).FirstOrDefault(p => p.id_produit == id);
            if (produit == null) return NotFound();
            return View(produit);
        }

        [HttpPost, ActionName("Supprimer")]
        public IActionResult SupprimerConfirme(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
