using BE_Biblioteca.Models;
using BE_Biblioteca.Data;
using BE_Biblioteca.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BE_Biblioteca.Services
{
    public class BookService
    {
        private readonly BibliotecaEfCoreDbContext _context;

        public BookService(BibliotecaEfCoreDbContext context)
        {
            _context = context;
        }

        public async Task<BookListViewModel> GetBooksAsync()
        {
            try
            {
                var bookList = new BookListViewModel();
                bookList.Books = await _context.Books.ToListAsync();
                return bookList;
            }
            catch
            {
                return new BookListViewModel() { Books = new List<Book>() };
            }
        }
    }
}
