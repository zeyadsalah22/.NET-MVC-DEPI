using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Day6Mydemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Day6MvcdbContext _context;
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }
        private string defaultPhoto = "default.jpeg";
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

        public ProductsController(Day6MvcdbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var listproduct = _context.Products.ToList();
            foreach(var item in listproduct)
            {
                if(!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", item.Photo)))
                {
                    item.Photo = defaultPhoto;
                }
            }
            return View(listproduct);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.Photo)))
            {
                product.Photo = defaultPhoto;
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([ModelBinder(BinderType = typeof(ProductBinder))] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["MessageAdd"] = $"Product {product.ProductName} Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.Photo)))
            {
                product.Photo = defaultPhoto;
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [ModelBinder(BinderType = typeof(ProductBinder))] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                TempData["MessageDelete"] = $"Product {product.ProductName} Deleted Successfully";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Gallery()
        {
            
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Card(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            if(!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.Photo)))
            {
                product.Photo = defaultPhoto;
            }

            return View(product);
        }

        public IActionResult CheckPrice(decimal Price)
        {
            if (Price < 100000)
            {
                return Json(true);
            }
            return Json(false);
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        public IActionResult CardAsPartialView(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            return PartialView("_ProductCardPartial", product);
        }

        public IActionResult CardAsJson(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            return Json(product);
        }
    }
}
