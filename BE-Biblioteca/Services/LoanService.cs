using BE_Biblioteca.Data;
using BE_Biblioteca.Models;
using BE_Biblioteca.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BE_Biblioteca.Services
{
    public class LoanService
    {
        private readonly BibliotecaEfCoreDbContext _context;
        private readonly BookService? _bookService;

        public LoanService(BibliotecaEfCoreDbContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Prestito>> GetLoansAsync()
        {
            List<Prestito> prestiti = await _context.Prestiti
                 .Where(p => p.DataRestituzione == null)
                 .Include(b => b.Book)
                 .ToListAsync();

            foreach (var prestito in prestiti)
            {
                prestito.PrestitoScaduto = prestito.LimiteRestituzione < DateTime.Now &&
                    prestito.DataRestituzione == null;
            }

            return prestiti;
        }

        public async Task<List<Prestito>> GetOverDueAsync()
        {
            var now = DateTime.Today;
            return await _context.Prestiti
                .Where(p => p.LimiteRestituzione < now && p.DataRestituzione == null)
                .Include(b => b.Book)
                .ToListAsync();
        }

        public async Task<List<Prestito>> GetReturnedAsync()
        {
            List<Prestito> prestiti = await _context.Prestiti
                .Where(p => p.DataRestituzione != null)
                .Include(b => b.Book)
                .ToListAsync();

            foreach (var prestito in prestiti)
            {
                prestito.PrestitoScaduto = prestito.LimiteRestituzione < DateTime.Now &&
                                           prestito.DataRestituzione == null;
            }

            return prestiti;
        }

        public async Task<Prestito> GetLoanByIdAsync(Guid id)
        {
            return await _context.Prestiti
                .Include(b => b.Book)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AddLoanAsync(Prestito prestito)
        {
            var book = await _bookService.GetBookByIdAsync(prestito.BookId);

            if (book == null || !book.Available)
            {
                throw new InvalidOperationException("Il libro non è disponibile per il prestito");
            }

            book.Available = false;
            await _bookService.UpdateBookAsync(book);

            _context.Prestiti.Add(prestito);

            var added = await SaveAsync();
            return added;
        }

        public async Task<bool> UpdateLoanAsync(Prestito prestito)
        {
            _context.Prestiti.Update(prestito);
            var updated = await SaveAsync();
            return updated;
        }

        public async Task<bool> ReturnLoanAsync(Guid id)
        {
            var prestito = await GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return false;
            }
            prestito.DataRestituzione = DateTime.Now;

            var book = await _bookService.GetBookByIdAsync(prestito.BookId);

            book.Available = true;

            await _bookService.UpdateBookAsync(book);

            _context.Prestiti.Update(prestito);

            var updated = await SaveAsync();
            return updated;
        }
    }
}
