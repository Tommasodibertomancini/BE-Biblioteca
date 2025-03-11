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

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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

        public async Task<bool> AddBookAsync(AddBookViewModel addBookViewModel)
        {
            try
            {
                var book = new Book()
                {
                    Title = addBookViewModel.Title,
                    Author = addBookViewModel.Author,
                    Genre = addBookViewModel.Genre,
                    Available = addBookViewModel.Available,
                    CoverUrl = addBookViewModel.CoverUrl
                };
                _context.Books.Add(book);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookDetailsViewModel> GetBookByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return new BookDetailsViewModel()
                {
                    Id = Guid.Empty,
                    Title = string.Empty,
                    Author = string.Empty,
                    Genre = string.Empty,
                    Available = false,
                    CoverUrl = string.Empty
                };
            }

            var bookDetails = new BookDetailsViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Available = book.Available,
                CoverUrl = book.CoverUrl
            };

            return bookDetails;
        }

        public async Task<EditBookViewModel> GetBookDetailsByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return new EditBookViewModel()
                {
                    Id = Guid.Empty,
                    Title = string.Empty,
                    Author = string.Empty,
                    Genre = string.Empty,
                    Available = false,
                    CoverUrl = string.Empty
                };
            }

            var bookDetails = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Available = book.Available,
                CoverUrl = book.CoverUrl
            };

            return bookDetails;
        }

        public async Task<bool> UpdateBookByIdAsync(EditBookViewModel edit)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == edit.Id);

            if (book == null)
            {
                return false;
            }

            book.Title = edit.Title;
            book.Author = edit.Author;
            book.Genre = edit.Genre;
            book.Available = edit.Available;
            book.CoverUrl = edit.CoverUrl;

            return await SaveAsync();
        }

        public async Task<bool> DeleteBookByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            return await SaveAsync();
        }
    }
}
