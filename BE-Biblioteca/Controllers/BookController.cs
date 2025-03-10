using BE_Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE_Biblioteca.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _productService;

        public BookController(BookService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetBooksAsync();
            return View(productList);
        }
    }
}
