using BE_Biblioteca.Services;
using BE_Biblioteca.ViewModel;
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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error while saving entity to database";
                return RedirectToAction("Index");
            }

            var result = await _productService.AddBookAsync(addBookViewModel);

            if (!result)
            {
                TempData["Error"] = "Error while saving entity to database";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Success"] = "Entity saved to database";
                return RedirectToAction("Index");
            }
        }

        [Route("product/details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _productService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _productService.GetBookDetailsByIdAsync(id);

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookViewModel editBookViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error while saving entity to database";
                return RedirectToAction("Index");
            }
            var result = await _productService.UpdateBookByIdAsync(editBookViewModel);
            if (!result)
            {
                TempData["Error"] = "Error while saving entity to database";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Success"] = "Entity saved to database";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteBookByIdAsync(id);

            if (!result)
            {
                TempData["Error"] = "Error while deleting entity from database";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Success"] = "Entity deleted from database";
                return RedirectToAction("Index");
            }
        }
    }
}
