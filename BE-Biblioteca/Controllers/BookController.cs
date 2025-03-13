using BE_Biblioteca.Services;
using BE_Biblioteca.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_Biblioteca.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _bookService.GetBooksAsync();
            return View(productList);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searched)
        {
            if (string.IsNullOrWhiteSpace(searched))
            {
                return RedirectToAction("Index");
            }

            var books = await _bookService.GetBookBySearchAsync(searched);
            return View("Index", books);
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

            var result = await _bookService.AddBookAsync(addBookViewModel);

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
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetBookDetailsByIdAsync(id);

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
            var result = await _bookService.UpdateBookByIdAsync(editBookViewModel);
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
            var result = await _bookService.DeleteBookByIdAsync(id);

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
