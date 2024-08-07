using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace library_management_1.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly LibraryFacade _libraryFacade;

        public ProductController(LibraryFacade libraryFacade)
        {
            _libraryFacade = libraryFacade;
        }

        public IActionResult Index(string searchId)
        {
            var products = _libraryFacade.GetAllProducts();

            if (!string.IsNullOrEmpty(searchId) && int.TryParse(searchId, out int id))
            {
                products = products.Where(p => p.Id == id).ToList();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var products = _libraryFacade.GetAllProducts();
            product.Id = GenerateNewProductId(products);
            var newProduct = _libraryFacade.CreateProduct(product.Id, product.BookName, product.Author);
            products.Add(newProduct);
            _libraryFacade.SaveProducts(products);
            return RedirectToAction("Index");
        }

        private int GenerateNewProductId(List<Product> products)
        {
            int maxId = products.Count > 0 ? products.Max(p => p.Id) : 0;
            return maxId + 1;
        }

        public IActionResult Edit(int id)
        {
            var product = _libraryFacade.GetAllProducts().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var products = _libraryFacade.GetAllProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.BookName = product.BookName;
                existingProduct.Author = product.Author;
                _libraryFacade.SaveProducts(products);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var products = _libraryFacade.GetAllProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                _libraryFacade.SaveProducts(products);
            }
            return RedirectToAction("Index");
        }
    }
}
